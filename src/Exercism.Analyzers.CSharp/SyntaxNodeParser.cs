using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Exercism.Analyzers.CSharp
{
    internal static class SyntaxNodeParser
    {
        public static SyntaxNode ParseNormalizedRoot(string code) =>
            CSharpSyntaxTree.ParseText(code).GetRoot().NormalizeWhitespace();
    }
}