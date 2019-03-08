using Microsoft.CodeAnalysis.Diagnostics;

namespace Exercism.Analyzers.CSharp.Old.Analyzers
{
    internal static class SyntaxNodeAnalysisContextExtensions
    {
        public static bool SkipAnalysis(this SyntaxNodeAnalysisContext context) =>
            context.Node.SkipAnalysis();
    }
}