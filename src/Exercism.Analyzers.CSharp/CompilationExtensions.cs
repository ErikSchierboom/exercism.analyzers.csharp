using System.Linq;
using Microsoft.CodeAnalysis;

namespace Exercism.Analyzers.CSharp
{
    internal static class CompilationExtensions
    {   
        public static bool HasErrors(this Compilation compilation) =>
            compilation.GetDiagnostics().Any(diagnostic => diagnostic.Severity == DiagnosticSeverity.Error);
    }
}