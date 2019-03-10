namespace Exercism.Analyzers.CSharp
{   
    public class Solution
    {   
        public string Track { get; }
        public string Exercise { get; }
        public string Directory { get; }

        public Solution(string track, string exercise, string directory)
        {
            Track = track;
            Exercise = exercise;
            Directory = directory;
        }
    }
}