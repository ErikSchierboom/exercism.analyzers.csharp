using System.IO;
using Humanizer;

namespace Exercism.Analyzers.CSharp
{
    public static class SolutionExtensions
    {
        public static string SolutionFilePath(this Solution solution) =>
            solution.FilePath(".solution.json");

        public static string AnalysisFilePath(this Solution solution) =>
            solution.FilePath("analysis.json");

        public static string ProjectFilePath(this Solution solution) =>
            solution.FilePath($"{solution.ExerciseFriendlyName()}.csproj");

        public static string ImplementationFilePath(this Solution solution) =>
            solution.FilePath($"{solution.ExerciseFriendlyName()}.cs");

        public static string FilePath(this Solution solution, string fileName) =>
            Path.GetFullPath(Path.Combine(solution.Directory, fileName));

        public static string ExerciseFriendlyName(this Solution solution) =>
            solution.Exercise.Dehumanize().Pascalize();
    }
}