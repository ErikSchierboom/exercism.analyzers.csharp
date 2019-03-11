using Serilog;

namespace Exercism.Analyzers.CSharp.Analyzers
{
    public static class DefaultAnalyzer
    {
        public static AnalyzedSolution Analyze(Solution solution)
        {
            Log.Information("Analysing {Exercise} using {Analyzer}", 
                solution.Exercise, nameof(DefaultAnalyzer));

            return new AnalyzedSolution(solution, SolutionStatus.ReferToMentor);
        }
    }
}