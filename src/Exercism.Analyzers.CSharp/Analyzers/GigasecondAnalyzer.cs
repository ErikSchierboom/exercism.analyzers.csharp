using Serilog;
using static Exercism.Analyzers.CSharp.Analyzers.GigasecondSolutions;

namespace Exercism.Analyzers.CSharp.Analyzers
{
    public static class GigasecondAnalyzer
    {
        public static AnalyzedSolution Analyze(Solution solution)
        {
            Log.Information("Analysing {Exercise} using {Analyzer}",
                solution.Exercise, nameof(GigasecondAnalyzer));

            if (solution.ImplementationIsEquivalentTo(AddSecondsWithScientificNotation))
                return new AnalyzedSolution(solution, SolutionStatus.Approve);

            if (solution.ImplementationIsEquivalentTo(AddSecondsWithMathPow))
                return new AnalyzedSolution(solution, SolutionStatus.Approve, "Use 1e9 instead of Math.Pow(10, 9)");

            if (solution.ImplementationIsEquivalentTo(AddSecondsWithDigitsWithoutSeparator))
                return new AnalyzedSolution(solution, SolutionStatus.Approve, "Use 1e9 or 1_000_000 instead of 1000000");

            if (solution.ImplementationIsEquivalentTo(AddSecondsWithScientificNotationInBlockBody))
                return new AnalyzedSolution(solution, SolutionStatus.Approve, "You could write the method an an expression-bodied member");

            if (solution.ImplementationIsEquivalentTo(Add))
                return new AnalyzedSolution(solution, SolutionStatus.ReferToMentor, "Use AddSeconds");

            if (solution.ImplementationIsEquivalentTo(PlusOperator))
                return new AnalyzedSolution(solution, SolutionStatus.ReferToMentor, "Use AddSeconds");

            return new AnalyzedSolution(solution, SolutionStatus.ReferToMentor);
        }
    }
}