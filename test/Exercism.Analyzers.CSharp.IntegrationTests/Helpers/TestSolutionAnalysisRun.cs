using Exercism.Analyzers.CSharp.Analyzers;

namespace Exercism.Analyzers.CSharp.IntegrationTests.Helpers
{
    public class TestSolutionAnalysisRun
    {
        public bool Success { get; }
        public bool Approved { get; }
        public bool ReferToMentor { get; }
        public string[] Messages { get; }

        public TestSolutionAnalysisRun(int returnCode)
        {
            Success = returnCode == 0;
        }

        public TestSolutionAnalysisRun(int returnCode, AnalysisResult analysisResult)
        {
            Success = returnCode == 0;
            Approved = analysisResult.Approve;
            ReferToMentor = analysisResult.ReferToMentor;
            Messages = analysisResult.Messages;
        }
    }
}