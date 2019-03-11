using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Exercism.Analyzers.CSharp
{
    public static class SolutionSyntaxExtensions
    {
        public static bool ImplementationIsEquivalentTo(this Solution solution, string expectedCode)
        {
            // TODO: don't keep on re-reading the implementation file
            
            var implementationCode = File.ReadAllText(solution.ImplementationFilePath());
            var implementationSyntaxNode = implementationCode.ToNormalizedSyntaxNode();
            var expectedSyntaxNode = expectedCode.ToNormalizedSyntaxNode();
            
            return implementationSyntaxNode.IsEquivalentTo(expectedSyntaxNode);
        }

        private static SyntaxNode ToNormalizedSyntaxNode(this string code) =>
            CSharpSyntaxTree.ParseText(code).GetRoot().NormalizeWhitespace();
    }
}