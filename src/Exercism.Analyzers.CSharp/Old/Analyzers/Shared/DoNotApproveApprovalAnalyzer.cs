using System.Threading.Tasks;
using Microsoft.CodeAnalysis;

namespace Exercism.Analyzers.CSharp.Analyzers.Shared
{
    public class DoNotApproveApprovalAnalyzer : ApprovalAnalyzer
    {
        public override Task<bool> CanBeApproved(Compilation compilation) => Task.FromResult(false);
    }
}