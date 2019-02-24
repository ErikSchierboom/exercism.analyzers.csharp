using Exercism.Analyzers.CSharp.Analysis.Compiling;
using Serilog;

namespace Exercism.Analyzers.CSharp.Analysis.Solutions
{
    public static class SolutionLoader
    {
        public static LoadedSolution Load(DownloadedSolution downloadedSolution)
        {
            Log.Information("Loading solution {ID} from {ProjectFile}",
                downloadedSolution.Solution.Id, downloadedSolution.ProjectFile.FullName);
            
            var project = ProjectLoader.LoadFromFile(downloadedSolution.ProjectFile);

            Log.Information("Loaded solution {ID}", downloadedSolution.Solution.Id);
            
            return new LoadedSolution(downloadedSolution.Solution, project);
        }
    }
}