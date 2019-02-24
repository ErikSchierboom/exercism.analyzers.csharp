using CommandLine;

namespace Exercism.Analyzers.CSharp
{
    public class Options
    {
        [Value(0, Required = true, HelpText = "Specify either the directory of the solution, or the UUID of the solution to download")]
        public string Argument { get; }

        public Options(string argument) => Argument = argument;
    }
}