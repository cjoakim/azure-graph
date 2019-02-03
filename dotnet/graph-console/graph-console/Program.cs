using System;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

// See https://docs.microsoft.com/en-us/dotnet/csharp/tutorials/console-webapiclient

namespace graph_console {

    public class Repo {

        public string name;
    }

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

            int impl = 3;

            if (impl == 1) {
                // Initial impl without JSON deserialization
                var stringTask = httpClient.GetStringAsync("https://api.github.com/orgs/dotnet/repos");
                var msg = await stringTask;
                Console.Write(msg);
            }

            if (impl == 2) {
                // 2nd impl
                var serializer = new DataContractJsonSerializer(typeof(List<Repo>));
                var streamTask = httpClient.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
                var repositories = serializer.ReadObject(await streamTask) as List<Repo>;
                foreach (var repo in repositories) {
                    Console.WriteLine(repo.name);
                }
            }

            if (impl == 3) {
                // 3rd impl
                var serializer = new DataContractJsonSerializer(typeof(List<Repository>));
                var streamTask = httpClient.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
                var repositories = serializer.ReadObject(await streamTask) as List<Repository>;
                foreach (var repo in repositories) {
                    Console.WriteLine(repo);
                }
            }
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
