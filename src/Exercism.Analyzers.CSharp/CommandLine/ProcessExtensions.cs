using System.Diagnostics;

namespace Exercism.Analyzers.CSharp.CommandLine
{
    internal static class ProcessExtensions
    {
        public static bool Failed(this Process process) => process.ExitCode != 0;
    }
}