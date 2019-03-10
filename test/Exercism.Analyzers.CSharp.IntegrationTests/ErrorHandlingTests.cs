using System.Threading.Tasks;
using Exercism.Analyzers.CSharp.IntegrationTests.Helpers;
using Xunit;

namespace Exercism.Analyzers.CSharp.IntegrationTests
{
    public class ErrorHandlingTests
    {
        [Fact]
        public async Task ReturnErrorCodeWhenSolutionIsNotCSharpSolution()
        {
            var testSolution = new TestSolution("leap", "ruby", "error-handling");
            var analysisRun = await TestSolutionAnalyzer.Run(testSolution, string.Empty);
            
            Assert.False(analysisRun.Success);
        }

        [Fact]
        public async Task DontApproveOrReferToMentorWhenCodeHasCompileErrors()
        {
            const string code = @"
                public static class Gigasecond  
                {
                    public static DateTime Add
                }";

            var analysisRun = await TestSolutionAnalyzer.Run("error-handling", code);
            
            Assert.True(analysisRun.Success);
            Assert.False(analysisRun.Approved);
            Assert.False(analysisRun.ReferToMentor);
            Assert.Single(analysisRun.Messages, "The code does not compile");
        }
    }
}