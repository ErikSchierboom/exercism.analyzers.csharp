using System.Linq;
using Exercism.Analyzers.CSharp.Old.Solutions;
using Microsoft.CodeAnalysis;

namespace Exercism.Analyzers.CSharp.Old.Analyzers
{
    internal static class CompilationExtensions
    {
        public static SyntaxTree GetImplementationSyntaxTree(this Compilation compilation, Exercise exercise) =>
            compilation.SyntaxTrees.FirstOrDefault(x => x.IsImplementationFile(exercise));
    }
}