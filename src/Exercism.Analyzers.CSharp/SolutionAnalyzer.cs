using System.Threading.Tasks;
using Exercism.Analyzers.CSharp.Analyzers;

namespace Exercism.Analyzers.CSharp
{
    public static class SolutionAnalyzer
    {
        public static async Task<AnalysisResult> Analyze(Solution solution)
        {
            var compilation = await SolutionCompiler.Compile(solution);
            if (compilation.HasErrors())
                return AnalysisResult.ReferToStudent(solution, "The code does not compile");
            
            switch (solution.Exercise)
            {
                case Exercises.Gigasecond: return GigasecondAnalyzer.Analyze(solution, compilation);
                case Exercises.Leap: return LeapAnalyzer.Analyze(solution, compilation);
                default: return DefaultAnalyzer.Analyze(solution, compilation);
            }
        }
    }
}