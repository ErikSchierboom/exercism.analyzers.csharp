namespace Exercism.Analyzers.CSharp.Solutions
{
    public class AnalysisResult
    {
        public SolutionStatus Status { get; }
        public string[] Comments { get; }

        public AnalysisResult(SolutionStatus status, string[] comments) => (Status, Comments) = (status, comments);
    }
}