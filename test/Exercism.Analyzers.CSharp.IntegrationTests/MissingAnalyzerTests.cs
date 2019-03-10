using System.Threading.Tasks;
using Xunit;

namespace Exercism.Analyzers.CSharp.IntegrationTests
{
    public class MissingAnalyzerTests
    {
        [Fact]
        public async Task ReferToMentorWhenNoAnalyzerHasBeenImplementedForExercise()
        {
            var analysisRun = await TestSolutionAnalyzer.Run("missing", string.Empty);
            
            Assert.True(analysisRun.Success);
            Assert.False(analysisRun.Approved);
            Assert.True(analysisRun.ReferToMentor);
            Assert.Empty(analysisRun.Messages);
        }
    }
}