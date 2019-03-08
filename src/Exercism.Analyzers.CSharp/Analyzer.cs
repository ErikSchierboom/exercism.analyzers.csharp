using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Buildalyzer;
using Buildalyzer.Workspaces;
using Exercism.Analyzers.CSharp.Analyzers;
using Humanizer;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Exercism.Analyzers.CSharp
{
    public readonly struct Solution
    {
        public readonly string Track;
        public readonly string Exercise;
        public readonly string ExerciseName;

        public Solution(string track, string exercise) =>
            (Track, Exercise, ExerciseName) = (track, exercise, exercise.Dehumanize().Pascalize());
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

            var projectFile = Path.Combine(directory, $"{solution.ExerciseName}.csproj");
            
            var project = new AnalyzerManager().GetProject(projectFile);
            var workspace = project.AddToWorkspace(new AdhocWorkspace());

            var compilation = await workspace.GetCompilationAsync();

            if (compilation.GetDiagnostics().Any(diagnostic => diagnostic.Severity == DiagnosticSeverity.Error))
                return 1;

            AnalysisResult analysisResult = new AnalysisResult();
            
            switch (solution.Exercise)
            {
                case "gigasecond":
                    analysisResult = GigasecondAnalyzer.Analyze(compilation);
                    break;
            }

            var analysisPath = Path.Combine(directory, "analysis.json");

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
            };

            File.WriteAllText(analysisPath, JsonConvert.SerializeObject(analysisResult, jsonSerializerSettings));

            return 0;
        }
    }
}
