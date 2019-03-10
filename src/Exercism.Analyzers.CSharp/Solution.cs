namespace Exercism.Analyzers.CSharp
{   
    public class Solution
    {   
        public string Exercise { get; }
        public string Track { get; }
        public string Directory { get; }

        public Solution(string exercise, string track, string directory) =>
            (Exercise, Track, Directory) = (exercise, track, directory);
    }
}