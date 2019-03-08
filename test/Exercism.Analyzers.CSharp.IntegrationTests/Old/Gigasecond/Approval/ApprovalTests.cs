using System.Threading.Tasks;
using Exercism.Analyzers.CSharp.IntegrationTests.Helpers;
using Xunit;

namespace Exercism.Analyzers.CSharp.IntegrationTests.Gigasecond.Approval
{
    public class ApprovalTests : SolutionAnalysisTests
    {
        public ApprovalTests() : base(FakeExercise.Gigasecond)
        {
        }

        [Fact]
        public async Task UsingAddSecondsWithExpressionBodiedMemberApprovedWithoutComments() =>
            await ApprovedWithoutComments();

        [Fact]
        public async Task UsingAddSecondsWithBlockApprovedWithComment() =>
            await ApprovedWithComment("The 'Add' method can be rewritten as an expression-bodied member.");

        [Fact]
        public async Task UsingAddSecondsWithBlockThatHasCommentsApprovedWithComment() =>
            await ApprovedWithComment("The 'Add' method can be rewritten as an expression-bodied member.");
    }
}