using System.Threading.Tasks;
using Exercism.Analyzers.CSharp.IntegrationTests.Helpers;
using Xunit;

namespace Exercism.Analyzers.CSharp.IntegrationTests.Leap.NumberOfChecks
{
    public class NumberOfChecksTests : SolutionAnalysisTests
    {
        public NumberOfChecksTests() : base(FakeExercise.Leap)
        {
        }

        [Fact]
        public async Task UsingMinimumNumberOfChecksRequiresMentoringWithoutComments() =>
            await RequiresMentoringWithoutComments();

        [Fact]
        public async Task UsingTooManyChecksRequiresChangeWithComment() =>
            await RequiresChangeWithComment("The 'IsLeapYear' method uses too many checks.");
    }
}