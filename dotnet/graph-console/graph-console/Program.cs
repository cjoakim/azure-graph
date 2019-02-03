using System;
using System.IO;

namespace graph_console {

    class Program {


        static void Main(string[] args) {

            log("Start of Main()");
            string accessToken = readAccessToken();
            log("accessToken: " + accessToken);

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
