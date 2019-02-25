using Exercism.Analyzers.CSharp.Compiling;
using Microsoft.CodeAnalysis;

namespace Exercism.Analyzers.CSharp.Testing
{
    internal static class CompilationExtensions
    {
        private static readonly RemoveSkipAttributeArgumentSyntaxRewriter RemoveSkipAttributeArgumentSyntaxRewriter = new RemoveSkipAttributeArgumentSyntaxRewriter();
        
        internal static Compilation EnableAllTests(this Compilation compilation) =>
            compilation.Rewrite(RemoveSkipAttributeArgumentSyntaxRewriter);
    }
}