using System.Threading.Tasks;
using Buildalyzer;
using Buildalyzer.Workspaces;
using Microsoft.CodeAnalysis;

namespace Exercism.Analyzers.CSharp
{
    public static class SolutionCompiler
    {
        public static async Task<Compilation> Compile(Solution solution)
        {
            var project = new AnalyzerManager().GetProject(solution.ProjectFile());
            var workspace = project.AddToWorkspace(new AdhocWorkspace());

            return await workspace.GetCompilationAsync();
        }
    }
}