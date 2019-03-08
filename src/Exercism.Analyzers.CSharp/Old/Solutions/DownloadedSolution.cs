using System.IO;

namespace Exercism.Analyzers.CSharp.Old.Solutions
{
    public class DownloadedSolution
    {
        public Solution Solution { get; }
        public FileInfo ProjectFile { get; }
        
        public DownloadedSolution(in Solution solution, FileInfo projectFile) =>
            (Solution, ProjectFile) = (solution, projectFile);
    }
}