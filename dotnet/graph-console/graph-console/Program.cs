using System;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

// See https://docs.microsoft.com/en-us/dotnet/csharp/tutorials/console-webapiclient

namespace graph_console {

    class Program {

        private static readonly HttpClient httpClient = new HttpClient();

        static void Main(string[] args) {

            log("Start of Main()");
            string accessToken = readAccessToken();
            log("accessToken: " + accessToken);

            getGithubDotnetRepositories().Wait();

            terminate();
        }

        static string readAccessToken() {

            // [Environment]::SetEnvironmentVariable("GRAPH_CONSOLE_HOME", "C:\Users\chris\github\azure-graph\dotnet\graph-console\graph-console", "User")
            string projectDir = Environment.GetEnvironmentVariable("GRAPH_CONSOLE_HOME");
            string inputFile  = projectDir + "/tmp/access_token.txt";
            log("inputFile: " + inputFile);

            using (StreamReader sr = new StreamReader(inputFile)) {
                return sr.ReadToEnd();
            }
        }

        private static async Task getGithubDotnetRepositories() {

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            httpClient.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var stringTask = httpClient.GetStringAsync("https://api.github.com/orgs/dotnet/repos");

            var msg = await stringTask;
            Console.Write(msg);
        }

        static void log(string msg) {

            Console.WriteLine(msg);
        }

        static void terminate() {

            log("Hit enter key to continue and terminate...");
            string line = Console.ReadLine();
            // log(line);
            // System.Threading.Thread.Sleep(1000);
        }
    }
}
