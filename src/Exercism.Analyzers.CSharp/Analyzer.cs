using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Buildalyzer;
using Buildalyzer.Workspaces;
using Humanizer;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;

namespace Exercism.Analyzers.CSharp
{
    public readonly struct Solution
    {
        public readonly string Track;
        public readonly string Exercise;
        public readonly string Name;

        public Solution(string track, string exercise) =>
            (Track, Exercise, Name) = (track, exercise, exercise.Dehumanize().Pascalize());
    }
    
    public static class Analyzer
    {
        public static async Task<int> Analyze(string directory)
        {
            var solutionPath = Path.Combine(directory, ".solution.json");

            // TODO: consider creating error codes class
            if (!File.Exists(solutionPath))
                return 1;

            // TODO: consider optimized read of solution
            var solution = JsonConvert.DeserializeObject<Solution>(File.ReadAllText(solutionPath));

            if (solution.Track != "csharp")
                return 1;

            var projectFile = Path.Combine(directory, $"{solution.Name}.csproj");
            
            var project = new AnalyzerManager().GetProject(projectFile);
            var workspace = project.AddToWorkspace(new AdhocWorkspace());

            var compilation = await workspace.GetCompilationAsync();

            if (compilation.GetDiagnostics().Any(diagnostic => diagnostic.Severity == DiagnosticSeverity.Error))
                return 1;
            
            

            return 0;
        }
        
//        public static async Task Analyze(string directory)
//        { 
//            var loadedSolution = await LoadSolution(directory);
//            var compiledSolution = await CompileSolution(loadedSolution);
//            var analysisResult = await AnalyzeSolution(compiledSolution);
//            await WriteAnalysisResultToFile(analysisResult);
//        }
//
//        private static async Task<LoadedSolution> LoadSolution(string id)
//        {
//            var downloadedSolution = await SolutionDownloader.Download(id);
//            return SolutionLoader.Load(downloadedSolution);
//        }
//
//        private static async Task<CompiledSolution> CompileSolution(LoadedSolution loadedSolution) =>
//            await SolutionCompiler.Compile(loadedSolution);
//
//        private static async Task<AnalysisResult> AnalyzeSolution(CompiledSolution compiledSolution)
//        {
//            var analyzedSolution = await SolutionAnalyzer.Analyze(compiledSolution);
//            return new AnalysisResult(analyzedSolution.Status, analyzedSolution.Comments);
//        }
//
//        private static async Task WriteAnalysisResultToFile(AnalysisResult analysisResult)
//        {
//            // TODO: write to file
//        }
    }
}
