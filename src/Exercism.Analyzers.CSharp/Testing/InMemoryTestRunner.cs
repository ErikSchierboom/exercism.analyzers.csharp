using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Xunit.Sdk;

namespace Exercism.Analyzers.CSharp.Testing
{
    internal static class InMemoryTestRunner
    {
        public static async Task<RunSummary> RunAll(Compilation compilation)
        {
            var compilationWithAllTestsEnabled = compilation.EnableAllTests();
            var assemblyInfo = compilationWithAllTestsEnabled.ToAssemblyInfo();

            using (var assemblyRunner = assemblyInfo.CreateTestAssemblyRunner())
                return await assemblyRunner.RunAsync();
        }
    }
}