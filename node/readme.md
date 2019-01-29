# Azure Graph API - with Node.js CLI Program

## Links

- https://github.com/microsoftgraph/msgraph-sdk-javascript

## Installation

Install the Node.js npm libraries as follows.
In particular, the **@microsoft/microsoft-graph-client SDK** is used.

```
$ cd graph/node/
$ npm install
```

## Obtain Token

- Go to the Microsoft Graph Explorer at https://developer.microsoft.com/en-us/graph/graph-explorer
- Login with the account you want to use to run the node samples
- Open the browser developer tools
- Type **tokenPlease()** into the console to get an access token
- Copy the access token from the console and put it into file **tmp/access_token.txt**
- Then run this program; see command-line options below

## Execution

```
node graph.js me
node graph.js my_selected_attrs
node graph.js my_photo
node graph.js users
node graph.js top_n_people 100
node graph.js job_titles 100
node graph.js search_users_by_name Joakim
```