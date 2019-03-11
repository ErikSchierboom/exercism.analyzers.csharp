using System.IO;

namespace Exercism.Analyzers.CSharp
{
    public static class SolutionPathExtensions
    {
        public static string SolutionFilePath(this Solution solution) =>
            solution.FilePath(".solution.json");

        public static string AnalysisFilePath(this Solution solution) =>
            solution.FilePath("analysis.json");

        public static string ImplementationFilePath(this Solution solution) =>
            solution.FilePath($"{solution.ExerciseFriendlyName()}.cs");

        private static string FilePath(this Solution solution, string fileName) =>
            Path.GetFullPath(Path.Combine(solution.Directory, fileName));
    }
}