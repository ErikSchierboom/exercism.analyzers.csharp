using Microsoft.CodeAnalysis.Diagnostics;

namespace Exercism.Analyzers.CSharp.Analyzers
{
    internal static class SyntaxNodeAnalysisContextExtensions
    {
        public static bool SkipAnalysis(this SyntaxNodeAnalysisContext context) =>
            context.Node.SkipAnalysis();
    }
}