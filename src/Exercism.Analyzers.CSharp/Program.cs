using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommandLine;
using Exercism.Analyzers.CSharp.Analysis;
using Exercism.Analyzers.CSharp.CommandLine;

namespace Exercism.Analyzers.CSharp
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Logging.Configure();

            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(HandleParseSuccess)
                .WithNotParsed(HandleParseErrors);
        }

        private static void HandleParseSuccess(Options options)
        {
            var directory = 
                Guid.TryParse(options.Argument, out var id)
                ? ExercismCommandLineInterface.Download(id)
                
            
            var analysisResult = Analyze();
            
            // TODO handle analysis
        }

        private static AnalysisResult Analyze() => Analyzer.Analyze("").GetAwaiter().GetResult();

        private static void HandleParseErrors(IEnumerable<Error> errors)
        {
            
        }
    }
}