using System;
using System.IO;
using System.Threading.Tasks;
using Serilog;

namespace Exercism.Analyzers.CSharp.CommandLine
{
    internal static class ExercismCommandLineInterface
    {   
        public static async Task<DirectoryInfo> Download(Guid id)
        {
            Log.Information("Running exercism CLI command for solution {ID}", id);
            
            var output = await CommandLineInterface.Run(GetExercismCliPath(), GetExercismCliArguments(id));

            Log.Information("Ran exercism CLI command for solution {ID}", id);
            
            return new DirectoryInfo(output.Trim());
        }

        private static string GetExercismCliPath() => PlatformSpecificBinary.GetPath("exercism");

        private static string GetExercismCliArguments(Guid id) => $"download -u {id}";
    }
}