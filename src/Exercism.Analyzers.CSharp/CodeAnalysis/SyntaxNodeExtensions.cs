using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Exercism.Analyzers.CSharp.CodeAnalysis
{
    internal static class SyntaxNodeExtensions
    {
        public static ClassDeclarationSyntax GetClassDeclaration(this SyntaxNode syntaxNode, string className) =>
            syntaxNode?
                .DescendantNodes()
                .OfType<ClassDeclarationSyntax>()
                .SingleOrDefault(syntax => syntax.Identifier.Text == className);

        public static MethodDeclarationSyntax GetMethodDeclaration(this SyntaxNode syntaxNode, string methodName) =>
            syntaxNode?
                .DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                .SingleOrDefault(syntax => syntax.Identifier.Text == methodName);
    }
}