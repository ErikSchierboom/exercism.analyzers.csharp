using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Exercism.Analyzers.CSharp.Testing
{
    internal static class AssemblyInfoExtensions
    {
        public static XunitTestAssemblyRunner CreateTestAssemblyRunner(this IAssemblyInfo assemblyInfo) =>
            new XunitTestAssemblyRunner(
                new TestAssembly(assemblyInfo),
                assemblyInfo.GetTestCases(),
                new Xunit.Sdk.NullMessageSink(),
                new Xunit.Sdk.NullMessageSink(),
                TestFrameworkOptions.ForExecution());

        private static IEnumerable<IXunitTestCase> GetTestCases(this IAssemblyInfo assemblyInfo)
        {
            using (var discoverySink = new TestDiscoverySink())
            using (var discoverer = new XunitTestFrameworkDiscoverer(assemblyInfo, new NullSourceInformationProvider(), new Xunit.Sdk.NullMessageSink()))
            {
                discoverer.Find(false, discoverySink, TestFrameworkOptions.ForDiscovery());
                discoverySink.Finished.WaitOne();

                return discoverySink.TestCases.Cast<IXunitTestCase>();
            }
        }
    }
}