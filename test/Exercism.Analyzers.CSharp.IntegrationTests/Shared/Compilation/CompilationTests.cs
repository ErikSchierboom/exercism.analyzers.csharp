using System.Threading.Tasks;
using Exercism.Analyzers.CSharp.IntegrationTests.Helpers;
using Xunit;

namespace Exercism.Analyzers.CSharp.IntegrationTests.Shared.Compilation
{
    public class CompilationTests : SolutionAnalysisTests
    {
        public CompilationTests() : base(FakeExercise.Shared)
        {
        }

        [Fact]
        public async Task CompilesWithoutErrorsRequiresMentoringWithoutComments() =>
            await RequiresMentoringWithoutComments();

        [Fact]
        public async Task CompilesWithErrorsRequiresChangeWithComment() =>
            await RequiresChangeWithSingleComment("The solution does not compile.");

        [Fact]
        public async Task CompilesWithErrorsAndNonErrorsRequiresChangeWithSingleComment() =>
            await RequiresChangeWithSingleComment("The solution does not compile.");
    }
}