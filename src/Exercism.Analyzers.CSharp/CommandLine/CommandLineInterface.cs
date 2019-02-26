using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Serilog;

namespace Exercism.Analyzers.CSharp.CommandLine
{
    internal static class CommandLineInterface
    {   
        public static async Task<string> Run(string path, string arguments)
        {
            using (var process = new Process {StartInfo = CreateStartInfo(path, arguments)})
            {
                Log.Information("Executing CLI command '{Path}' with arguments '{Arguments}'",
                    process.StartInfo.FileName, process.StartInfo.Arguments);

                process.Start();
                process.WaitForExit();
                
                if (process.Failed())
                {
                    var error = await process.StandardError.ReadToEndAsync();
                    Log.Error("An error occured while executing CLI command '{Path}' with arguments '{Arguments}': '{Error}'", 
                        process.StartInfo.FileName, process.StartInfo.Arguments,error);
                    throw new InvalidOperationException(error);
                }
                
                var output = await process.StandardOutput.ReadToEndAsync();
                Log.Information("Executed CLI command '{Path}' with arguments '{Arguments}': '{Output}'",
                    process.StartInfo.FileName, process.StartInfo.Arguments, output);
                
                return output;
            }
        }

        private static ProcessStartInfo CreateStartInfo(string fileName, string arguments) =>
            new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };
    }
}