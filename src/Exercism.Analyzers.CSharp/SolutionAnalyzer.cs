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
                return new AnalysisResult(solution, approve: false, referToMentor: false, messages: "The code does not compile");
            
            switch (solution.Exercise)
            {
                case Exercises.Gigasecond:
                    return await GigasecondAnalyzer.Analyze(solution, compilation);
                default:
                    return new AnalysisResult(solution, approve: false, referToMentor: true);
            }
        }
    }
}