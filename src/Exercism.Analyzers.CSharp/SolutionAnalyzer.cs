using System.Threading.Tasks;
using Exercism.Analyzers.CSharp.Analyzers;

namespace Exercism.Analyzers.CSharp
{
    public static class SolutionAnalyzer
    {
        public static async Task<AnalyzedSolution> Analyze(Solution solution)
        {   
            switch (solution.Exercise)
            {
                case Exercises.Gigasecond: return GigasecondAnalyzer.Analyze(solution);
                case Exercises.Leap: return LeapAnalyzer.Analyze(solution);
                default: return DefaultAnalyzer.Analyze(solution);
            }
        }
    }
}