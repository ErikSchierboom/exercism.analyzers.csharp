using System.IO;

namespace Exercism.Analyzers.CSharp.IntegrationTests.Helpers
{
    public class TestSolution : Solution
    {
        public TestSolution(string exercise) : this(exercise, "csharp")
        {
        }

        public TestSolution(string exercise, string track) : base(exercise, track, Path.Combine("solutions", exercise))
        {
        }

        public void CreateFiles(string code)
        {
            CreateDirectory();
            CreateImplementationFile(code);
            CreateSolutionFile();
        }

        private void CreateDirectory()
        {
            if (System.IO.Directory.Exists(Directory))
                System.IO.Directory.Delete(Directory, recursive: true);

            System.IO.Directory.CreateDirectory(Directory);
        }

        private void CreateImplementationFile(string code) =>
            CreateFile(this.ImplementationFilePath(), code);

        private void CreateSolutionFile() =>
            CreateFile(this.SolutionFilePath(),$"{{\"track\":\"{Track}\",\"exercise\":\"{Exercise}\"}}");

        private static void CreateFile(string path, string contents) =>
            File.WriteAllText(path, contents);
    }
}