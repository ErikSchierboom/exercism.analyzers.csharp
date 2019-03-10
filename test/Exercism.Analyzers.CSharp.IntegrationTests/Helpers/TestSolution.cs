using System.IO;

namespace Exercism.Analyzers.CSharp.IntegrationTests.Helpers
{
    public class TestSolution : Solution
    {
        private const string ProjectFileContents = @"
<Project Sdk=""Microsoft.NET.Sdk"">
    <PropertyGroup>
        <TargetFramework>netcoreapp2.2</TargetFramework>
    </PropertyGroup>
</Project >";

        public TestSolution(string exercise) : this(exercise, "csharp")
        {
        }

        public TestSolution(string exercise, string track) : base(exercise, track, Path.Combine("solutions", exercise))
        {
        }

        public TestSolution(string exercise, string track, string directory) : base(exercise, track, Path.Combine("solutions", directory))
        {
        }

        public void CreateFiles(string code)
        {
            CreateDirectory();
            CreateProjectFile();
            CreateImplementationFile(code);
            CreateSolutionFile();
        }

        private void CreateDirectory()
        {
            if (System.IO.Directory.Exists(Directory))
                System.IO.Directory.Delete(Directory, recursive: true);

            System.IO.Directory.CreateDirectory(Directory);
        }

        private void CreateProjectFile() =>
            CreateFile(this.ProjectFile(), ProjectFileContents);

        private void CreateImplementationFile(string code) =>
            CreateFile(this.ImplementationFile(), code);

        private void CreateSolutionFile() =>
            CreateFile(this.SolutionFile(),$"{{\"track\":\"{Track}\",\"exercise\":\"{Exercise}\"}}");

        private static void CreateFile(string path, string contents) =>
            File.WriteAllText(path, contents);
    }
}