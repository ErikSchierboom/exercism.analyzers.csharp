using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Exercism.Analyzers.CSharp.Analysis.CommandLine
{
    internal static class CommandLineInterface
    {   
        public static async Task<CommandLineInterfaceResult> Run(string fileName, string arguments)
        {
            using (var process = new Process {StartInfo = CreateStartInfo(fileName, arguments)})
            {
                Log.Information("Executing CLI command '{File}' with arguments '{Arguments}'",
                    process.StartInfo.FileName, process.StartInfo.Arguments);
                
                process.Start();
                process.WaitForExit();
                
                Log.Information("Executed CLI command '{File}' with arguments '{Arguments}'",
                    process.StartInfo.FileName, process.StartInfo.Arguments);
                
                var output = await process.StandardOutput.ReadToEndAsync();
                var error = await process.StandardError.ReadToEndAsync();
                
                if (process.ExitCode == 0)
                    Log.Information("Output of executed CLI command: '{Output}'", output);
                else
                    Log.Error("Error output of executed CLI command: '{Error}'", error);

                return new CommandLineInterfaceResult(output, error, process.ExitCode);
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