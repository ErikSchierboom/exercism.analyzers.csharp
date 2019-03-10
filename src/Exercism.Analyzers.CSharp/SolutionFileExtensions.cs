using System.IO;
using Humanizer;

namespace Exercism.Analyzers.CSharp
{
    public static class SolutionFileExtensions
    {
        public static string SolutionFile(this Solution solution) =>
            Path.Combine(solution.Directory, ".solution.json");

        public static string AnalysisFile(this Solution solution) =>
            Path.Combine(solution.Directory, "analysis.json");

        public static string ProjectFile(this Solution solution) =>
            Path.Combine(solution.Directory, $"{solution.Exercise.Dehumanize().Pascalize()}.csproj");

        public static string ImplementationFile(this Solution solution) =>
            Path.Combine(solution.Directory, $"{solution.Exercise.Dehumanize().Pascalize()}.cs");
    }
}