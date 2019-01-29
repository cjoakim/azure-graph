// Node.js command-line utility to invoke the Azure Graph API
// Chris Joakim, Microsoft, 2019/01/29
//
// See https://github.com/microsoftgraph/msgraph-sdk-javascript/tree/dev/samples/node
// Modified instructions, per that page, for this utility program:
// 1) Go to the Microsoft Graph Explorer ( https://developer.microsoft.com/en-us/graph/graph-explorer )
// 2) Login with the account you want to use to run the node samples
// 3) Open the browser developer tools
// 4) Type 'tokenPlease()' into the console to get an access token
// 5) Copy the access token from the console and put it into file 'tmp/access_token.txt'
// 6) Then run this program; see command-line options below

// Require the npm libraries
const fs = require("fs");
const MicrosoftGraph = require("@microsoft/microsoft-graph-client").Client;

// Read your access token obtained from Microsoft Graph Explorer in your browser
// If you clone/fork this repo, be sure to have a .gitignore on this access_token.txt file!
const access_token = fs.readFileSync('tmp/access_token.txt', 'utf8');

// Create a MicrosoftGraph SDK client using your access token
const client = MicrosoftGraph.init({
    defaultVersion: 'v1.0',
    debugLogging: true,
    authProvider: (done) => {
        done(null, access_token);
    }
});

if (process.argv.length < 3) {
    console.log('Invalid program args; please specify a runtime function as follows:');
    console.log('node graph.js me');
    console.log('node graph.js my_selected_attrs');
    console.log('node graph.js my_photo');
    console.log('node graph.js users');
    console.log('node graph.js top_n_people 100');
    console.log('node graph.js job_titles 100');
    console.log('node graph.js search_users_by_name Joakim');
    console.log('');
    process.exit();
}

function handleResponse(funct, res) {
    var jstr = JSON.stringify(res, null, 2);
    console.log(jstr);
    writeFile(funct, jstr);
}

function handleCollectJobTitles(funct, res) {
    var jstr = JSON.stringify(res, null, 2);
    console.log(jstr);
    writeFile(funct + '_raw', jstr);

    jobTitles = {};
    for (var i = 0; i < res.value.length; i++) {
        var person = res.value[i];
        var title  = person['jobTitle'];
        var count  = 1;
        if (jobTitles.hasOwnProperty(title)) {
            count = jobTitles[title];
            jobTitles[title] = count + 1;
        }
        else {
            jobTitles[title] = count;
        }
    }
    var jstr = JSON.stringify(jobTitles, null, 2);
    console.log(jstr);
    writeFile(funct + '_distinct', jstr);
}

function writeFile(funct, content) {
    var outfile = 'tmp/' + funct + '.json';
    fs.writeFileSync(outfile, content);
    console.log('file written: ' + outfile);
}

const funct = process.argv[2];

if (funct === 'me') {
    client
        .api('/me')
        .get()
        .then((res) => {
            handleResponse(funct, res);
        }).catch((err) => {
            console.log(err);
        });
}

else if (funct === 'my_selected_attrs') {
    client
        .api('/me')
        .select("displayName,mail,jobTitle")
        .get()
        .then((res) => {
            handleResponse(funct, res);
        }).catch((err) => {
            console.log(err);
        });
}

else if (funct === 'my_photo') {
    client
        .api('me/photo/$value')
        .getStream((err, downloadStream) => {
            var outfile = 'tmp/my_photo.jpg';
            let writeStream = fs.createWriteStream(outfile);
            downloadStream.pipe(writeStream).on('error', console.log);
            console.log('file written: ' + outfile);
        });
}

else if (funct === 'users') {
    client
        .api('/users')
        .get()
        .then((res) => {
            handleResponse(funct, res);
        }).catch((err) => {
            console.log(err);
        });
}

else if (funct === 'search_users_by_name') {
    var name = process.argv[3];
    var api_path = 'me/people/?$search="' + name + '"';
    client
        .api(api_path)
        .get()
        .then((res) => {
            handleResponse(funct, res);
        }).catch((err) => {
            console.log(err);
        });
}

else if (funct === 'top_n_people') {
    var n = process.argv[3];
    var api_path = '/me/people?$top=' + n;
    client
        .api(api_path)
        .get()
        .then((res) => {
            handleResponse(funct + '_' + n, res);
            console.log('response count: ' + res.value.length);
        }).catch((err) => {
            console.log(err);
        });
}

else if (funct === 'job_titles') {
    var n = process.argv[3];
    var api_path = '/me/people?$top=' + n;
    client
        .api(api_path)
        .select("displayName,userPrincipalName,jobTitle")
        .get()
        .then((res) => {
            handleCollectJobTitles(funct, res)
        }).catch((err) => {
            console.log(err);
        });
}

else {
    console.log('unknown function: ' + funct);
}

// Notes:
// .select(["displayName", "title"]) or .select("displayName", "title")
