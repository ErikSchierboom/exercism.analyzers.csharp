using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Exercism.Analyzers.CSharp.Analyzers;
using Exercism.Analyzers.CSharp.Compiling;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Serilog;

namespace Exercism.Analyzers.CSharp.Solutions
{
    public static class SolutionAnalyzer
    {
        public static async Task<AnalyzedSolution> Analyze(CompiledSolution compiledSolution)
        {
            var analyzedSolution = await CreateAnalyzedSolution(compiledSolution);

            Log.Information("Analysis result for solution {ID}: status {SolutionStatus}, comments {Comments}", 
                compiledSolution.Solution.Id, analyzedSolution.Status, analyzedSolution.Comments);

            return analyzedSolution;
        }

        private static async Task<AnalyzedSolution> CreateAnalyzedSolution(CompiledSolution compiledSolution)
        {
            if (HasCompilationErrors(compiledSolution))
                return CreateAnalyzedSolutionForCompilationErrors(compiledSolution);

            return await CreateAnalyzedSolutionForCorrectSolution(compiledSolution);
        }

        private static bool HasCompilationErrors(CompiledSolution compiledSolution) =>
            compiledSolution.Compilation.HasErrors();

        private static AnalyzedSolution CreateAnalyzedSolutionForCompilationErrors(CompiledSolution compiledSolution) =>
            AnalyzedSolution.CreateRequiresChange(compiledSolution.Solution, "The solution does not compile.");

        private static async Task<AnalyzedSolution> CreateAnalyzedSolutionForCorrectSolution(CompiledSolution compiledSolution)
        {
            var diagnostics = await GetDiagnostics(compiledSolution);
            var diagnosticsBySeverity = diagnostics.ToLookup(diagnostic => diagnostic.Severity);

            Log.Information(
                "Retrieved diagnostics for solution {ID}: {NumberOfErrors} errors, {NumberOfWarnings} warnings and {NumberOfInformation} information",
                compiledSolution.Solution.Id,
                diagnosticsBySeverity[DiagnosticSeverity.Error].Count(),
                diagnosticsBySeverity[DiagnosticSeverity.Warning].Count(),
                diagnosticsBySeverity[DiagnosticSeverity.Info].Count());

            var errors = diagnosticsBySeverity[DiagnosticSeverity.Error].ToImmutableArray();
            if (errors.Any())
                return AnalyzedSolution.CreateRequiresChange(compiledSolution.Solution, GetDiagnosticMessages(errors));

            var comments = GetDiagnosticMessages(diagnostics);

            if (await CanBeApproved(compiledSolution))
                return AnalyzedSolution.CreateApproved(compiledSolution.Solution, comments);

            return AnalyzedSolution.CreateRequiresMentoring(compiledSolution.Solution, comments);
        }

        private static async Task<ImmutableArray<Diagnostic>> GetDiagnostics(CompiledSolution compiledSolution)
        {
            var analyzers = AnalyzerFactory.Create(compiledSolution.Solution);

            Log.Information("Retrieving diagnostics for solution {ID} using analyzers {Analyzers}",
                compiledSolution.Solution.Id, GetAnalyzerNames(analyzers));

            var compilationWithAnalyzers = compiledSolution.Compilation.WithAnalyzers(analyzers);
            return await compilationWithAnalyzers.GetAnalyzerDiagnosticsAsync();
        }

        private static async Task<bool> CanBeApproved(CompiledSolution compiledSolution)
        {
            Log.Information("Checking if solution {ID} can be automatically approved",
                compiledSolution.Solution.Id);
            
            var approvalAnalyzer = AnalyzerFactory.CreateForApproval(compiledSolution.Solution);
            if (!await approvalAnalyzer.CanBeApproved(compiledSolution.Compilation))
                return false;

            Log.Information("Solution {ID} can be automatically approved",
                compiledSolution.Solution.Id);

            return true;
        }

        private static string[] GetDiagnosticMessages(IEnumerable<Diagnostic> diagnostics) =>
            diagnostics.Select(diagnostic => diagnostic.GetMessage()).ToArray();

        private static IEnumerable<string> GetAnalyzerNames(ImmutableArray<DiagnosticAnalyzer> analyzers) =>
            analyzers.Select(analyzer => analyzer.GetType().Name);
    }
}