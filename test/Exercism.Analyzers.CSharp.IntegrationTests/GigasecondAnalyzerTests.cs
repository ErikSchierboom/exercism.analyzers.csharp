using System.Threading.Tasks;
using Exercism.Analyzers.CSharp.Analyzers;
using Exercism.Analyzers.CSharp.IntegrationTests.Helpers;
using Xunit;

namespace Exercism.Analyzers.CSharp.IntegrationTests
{
    public class GigasecondAnalyzerTests
    {
        private const string Exercise = "gigasecond";

        [Fact]
        public async Task ApproveWhenUsingAddSecondsWithScientificNotation()
        {
            const string code = @"
                using System;
                
                public static class Gigasecond
                {
                    public static DateTime Add(DateTime birthDate) => birthDate.AddSeconds(1e9);
                }";

            var analysisRun = await TestSolutionAnalyzer.Run(Exercise, code);
            
            Assert.True(analysisRun.Success);
            Assert.Equal(SolutionStatus.Approve, analysisRun.Status);
            Assert.Empty(analysisRun.Messages);
        }

        [Fact]
        public async Task ApproveWithMessageWhenUsingAddSecondsWithMathPow()
        {
            const string code = @"
                using System;
                
                public static class Gigasecond
                {
                    public static DateTime Add(DateTime birthDate) => birthDate.AddSeconds(Math.Pow(10, 9));
                }";

            var analysisRun = await TestSolutionAnalyzer.Run(Exercise, code);
            
            Assert.True(analysisRun.Success);
            Assert.Equal(SolutionStatus.Approve, analysisRun.Status);
            Assert.Single(analysisRun.Messages, "Use 1e9 instead of Math.Pow(10, 9)");
        }

        [Fact]
        public async Task ApproveWithMessageWhenUsingAddSecondsWithDigitsWithoutSeparator()
        {
            const string code = @"
                using System;
                
                public static class Gigasecond
                {
                    public static DateTime Add(DateTime birthDate) => birthDate.AddSeconds(1000000);
                }";

            var analysisRun = await TestSolutionAnalyzer.Run(Exercise, code);
            
            Assert.True(analysisRun.Success);
            Assert.Equal(SolutionStatus.Approve, analysisRun.Status);
            Assert.Single(analysisRun.Messages, "Use 1e9 or 1_000_000 instead of 1000000");
        }

        [Fact]
        public async Task ApproveWithMessageWhenUsingAddSecondsWithScientificNotationInBlockBody()
        {
            const string code = @"
                using System;
                
                public static class Gigasecond
                {
                    public static DateTime Add(DateTime birthDate)
                    {
                        return birthDate.AddSeconds(1e9);
                    }
                }";

            var analysisRun = await TestSolutionAnalyzer.Run(Exercise, code);
            
            Assert.True(analysisRun.Success);
            Assert.Equal(SolutionStatus.Approve, analysisRun.Status);
            Assert.Single(analysisRun.Messages, "You could write the method an an expression-bodied member");
        }

        [Fact]
        public async Task ReferToMentorWithMessageWhenUsingAdd()
        {
            const string code = @"
                using System;
                
                public static class Gigasecond
                {
                    public static DateTime Add(DateTime birthDate) => birthDate.Add(TimeSpan.FromSeconds(1000000000));
                }";

            var analysisRun = await TestSolutionAnalyzer.Run(Exercise, code);
            
            Assert.True(analysisRun.Success);
            Assert.Equal(SolutionStatus.ReferToMentor, analysisRun.Status);
            Assert.Single(analysisRun.Messages, "Use AddSeconds");
        }

        [Fact]
        public async Task ReferToMentorWithMessageWhenUsingPlusOperator()
        {
            const string code = @"
                using System;
                
                public static class Gigasecond
                {
                    public static DateTime Add(DateTime birthDate) => birthDate + TimeSpan.FromSeconds(1000000000);
                }";

            var analysisRun = await TestSolutionAnalyzer.Run(Exercise, code);
            
            Assert.True(analysisRun.Success);
            Assert.Equal(SolutionStatus.ReferToMentor, analysisRun.Status);
            Assert.Single(analysisRun.Messages, "Use AddSeconds");
        }
    }
}