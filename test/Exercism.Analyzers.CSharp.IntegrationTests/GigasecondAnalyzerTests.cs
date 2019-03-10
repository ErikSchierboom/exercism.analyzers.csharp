using System.Threading.Tasks;
using Xunit;

namespace Exercism.Analyzers.CSharp.IntegrationTests
{
    public class GigasecondAnalyzerTests
    {
        private const string Exercise = "gigasecond";

        [Fact]
        public async Task ApproveAddSecondsWithScientificNotationWithoutMessages()
        {
            const string code = @"
                using System;
                
                public static class Gigasecond
                {
                    public static DateTime Add(DateTime birthDate) => birthDate.AddSeconds(1e9);
                }";

            var analysisRun = await TestSolutionAnalyzer.Run(Exercise, code);
            
            Assert.True(analysisRun.Success);
            Assert.True(analysisRun.Approved);
            Assert.False(analysisRun.ReferToMentor);
            Assert.Empty(analysisRun.Messages);
        }
    }
}