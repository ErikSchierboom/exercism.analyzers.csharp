using Exercism.Analyzers.CSharp.Compiling;
using Microsoft.CodeAnalysis;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Exercism.Analyzers.CSharp.Testing
{
    internal static class CompilationExtensions
    {
        public static Compilation EnableAllTests(this Compilation compilation) =>
            compilation.Rewrite(new RemoveSkipAttributeFromTestsRewriter());
        
        public static IReflectionAssemblyInfo ToAssemblyInfo(this Compilation compilation) =>
            Reflector.Wrap(compilation.GetAssembly());
    }
}