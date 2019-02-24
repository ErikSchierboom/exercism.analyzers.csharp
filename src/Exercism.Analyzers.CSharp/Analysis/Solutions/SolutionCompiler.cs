using System.Threading.Tasks;
using Serilog;

namespace Exercism.Analyzers.CSharp.Analysis.Solutions
{
    public static class SolutionCompiler
    {
        public static async Task<CompiledSolution> Compile(LoadedSolution loadedSolution)
        {
            Log.Information("Compiling solution {ID}",
                loadedSolution.Solution.Id, loadedSolution.Solution.Id);
            
            var compilation = await loadedSolution.Project.GetCompilationAsync();
            
            Log.Information("Compiled solution {ID}", loadedSolution.Solution.Id);
            
            return new CompiledSolution(loadedSolution.Solution, compilation);
        }
    }
}