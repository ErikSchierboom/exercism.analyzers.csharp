using System;
using CommandLine;

namespace Exercism.Analyzers.CSharp
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Logging.Configure();
            
            var result = Parser.Default.ParseArguments<Options>(args);

            Console.WriteLine(result);
            // TODO: use command-line parsing library
            // TODO: analyze solution
        }
    }
    
    public class Options
    {
        [Value(0)]
        [Option('d', "directory", Required = false, HelpText = "The directory containing the solution to analyze.")]
        public string Directory { get; }
        
        [Option('u', "uuid", Required = false, HelpText = "The directory containing the solution to analyze.")]
        public Guid? Uuid { get; }

        public Options(string directory, Guid? uuid) => (Directory, Uuid) = (directory, uuid);
    }
}