using Microsoft.CodeAnalysis;
using Serilog;

namespace Exercism.Analyzers.CSharp.Analyzers
{
    public static class DefaultAnalyzer
    {
        public static AnalysisResult Analyze(Solution solution, Compilation _)
        {
            Log.Information("Analysing {Exercise} using {Analyzer}", 
                solution.Exercise, nameof(DefaultAnalyzer));

            return AnalysisResult.ReferToMentor(solution);
        }
    }
}