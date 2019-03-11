using Humanizer;

namespace Exercism.Analyzers.CSharp
{
    public static class SolutionExtensions
    {
        public static string ExerciseFriendlyName(this Solution solution) =>
            solution.Exercise.Dehumanize().Pascalize();
    }
}