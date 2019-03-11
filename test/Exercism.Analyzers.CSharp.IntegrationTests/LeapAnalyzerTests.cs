using System.Threading.Tasks;
using Exercism.Analyzers.CSharp.Analyzers;
using Exercism.Analyzers.CSharp.IntegrationTests.Helpers;
using Xunit;

namespace Exercism.Analyzers.CSharp.IntegrationTests
{
    public class LeapAnalyzerTests
    {
        private const string Exercise = "leap";

        [Fact]
        public async Task ApproveWhenUsingMinimumNumberOfChecks()
        {
            const string code = @"
                public static class Leap
                {
                    public static bool IsLeapYear(int year) =>
                        year % 4 == 0 && year % 100 != 0 || year % 400 == 0;
                }";

            var analysisRun = await TestSolutionAnalyzer.Run(Exercise, code);
            
            Assert.True(analysisRun.Success);
            Assert.Equal(SolutionStatus.Approve, analysisRun.Status);
            Assert.Empty(analysisRun.Messages);
        }

        [Fact]
        public async Task ApproveWhenUsingUnneededParentheses()
        {
            const string code = @"
                public static class Leap
                {
                    public static bool IsLeapYear(int year) =>
                        (year % 4 == 0) && ((year % 100 != 0) || (year % 400 == 0));
                }";

            var analysisRun = await TestSolutionAnalyzer.Run(Exercise, code);
            
            Assert.True(analysisRun.Success);
            Assert.Equal(SolutionStatus.Approve, analysisRun.Status);
            Assert.Empty(analysisRun.Messages);
        }

        [Fact]
        public async Task ApproveWithMessageWhenUsingMethodWithBlockBody()
        {
            const string code = @"
                public static class Leap
                {
                    public static bool IsLeapYear(int year)
                    {
                        return year % 4 == 0 && year % 100 != 0 || year % 400 == 0;
                    }
                }";

            var analysisRun = await TestSolutionAnalyzer.Run(Exercise, code);
            
            Assert.True(analysisRun.Success);
            Assert.Equal(SolutionStatus.Approve, analysisRun.Status);
            Assert.Single(analysisRun.Messages, "You could write the method an an expression-bodied member");
        }

        [Fact]
        public async Task ReferToMentorWithMessageWhenUsingTooManyChecks()
        {
            const string code = @"
                public static class Leap
                {
                    public static bool IsLeapYear(int year) =>
                        year % 4 == 0 && year % 100 != 0 || year % 100 == 0 && year % 400 == 0;
                }";

            var analysisRun = await TestSolutionAnalyzer.Run(Exercise, code);
            
            Assert.True(analysisRun.Success);
            Assert.Equal(SolutionStatus.ReferToMentor, analysisRun.Status);
            Assert.Single(analysisRun.Messages, "Use minimum number of checks");
        }
    }
}