using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Exercism.Analyzers.CSharp.Solutions
{
    public static class SolutionDownloader
    {
        public static async Task<DownloadedSolution> Download(string argument)
        {   
            var solutionDirectory = new DirectoryInfo(argument);
            
            var solution = await GetSolution(solutionDirectory);
            var projectFile = GetProjectFile(solution, solutionDirectory);
            
            return new DownloadedSolution(solution, projectFile);
        }

        private static async Task<Solution> GetSolution(FileSystemInfo solutionDirectory)
        {
            using (var textReader = GetMetadataFile(solutionDirectory).OpenText())
            using (var jsonTextReader = new JsonTextReader(textReader))
            {
                var solutionMetadata = await JToken.ReadFromAsync(jsonTextReader);
                var id = solutionMetadata.Value<string>("id");
                var slug = solutionMetadata.Value<string>("exercise");

                var exercise = new Exercise(slug);
                return new Solution(id, exercise);
            }
        }
        
        private static FileInfo GetMetadataFile(FileSystemInfo solutionDirectory) =>
            GetFileInSolutionDirectory(solutionDirectory, Path.Combine(".exercism", "metadata.json"));

        private static FileInfo GetProjectFile(in Solution solution, FileSystemInfo solutionDirectory) =>
            GetFileInSolutionDirectory(solutionDirectory, $"{solution.Exercise.Name}.csproj");

        private static FileInfo GetFileInSolutionDirectory(FileSystemInfo solutionDirectory, string solutionFile) =>
            new FileInfo(Path.Combine(solutionDirectory.FullName, solutionFile));
    }
}