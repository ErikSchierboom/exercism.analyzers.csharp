using System.Linq;
using Microsoft.CodeAnalysis;

namespace Exercism.Analyzers.CSharp.CodeAnalysis
{
    internal static class CompilationExtensions
    {
        public static SyntaxTree ImplementationSyntaxTree(this Compilation compilation, Solution solution) =>
            compilation.SyntaxTrees.FirstOrDefault(x => x.FileName() == solution.ImplementationFile());
        
        public static bool HasErrors(this Compilation compilation) =>
            compilation.GetDiagnostics().Any(diagnostic => diagnostic.Severity == DiagnosticSeverity.Error);
    }
}