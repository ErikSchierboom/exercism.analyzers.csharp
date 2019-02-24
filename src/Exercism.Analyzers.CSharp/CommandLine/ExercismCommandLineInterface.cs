using System;
using System.IO;
using System.Threading.Tasks;
using Serilog;

namespace Exercism.Analyzers.CSharp.CommandLine
{
    internal static class ExercismCommandLineInterface
    {   
        public static async Task<DirectoryInfo> Download(string id)
        {
            var arguments = GetArguments(id);

            Log.Information("Executing exercism CLI command for solution {ID}: {Command}", id, arguments);
            
            var output = await CommandLineInterface.Run(GetFileName(), arguments);

            Log.Information("Executed exercism CLI command for solution {ID}", id);
            
            return new DirectoryInfo(output.Output.Trim());
        }

        private static string GetArguments(string id) => $"download -u {id}";
        
        private static string GetFileName() =>
            Path.Combine("binaries", $"{GetOperatingSystem()}-{GetArchitecture()}", "exercism");
        
        private static string GetOperatingSystem()
        {
            if (OperatingSystem.IsWindows())
                return "win";
            if (OperatingSystem.IsLinux())
                return "linux";
            if (OperatingSystem.IsMacOs())
                return "osx";
            
            throw new InvalidOperationException("Unsupported operating system");
        }

        private static string GetArchitecture()
        {
            if (Architecture.IsX86())
                return "x86";
            if (Architecture.IsX64())
                return "x64";

            throw new InvalidOperationException("Unsupported architecture system");
        }
    }
}