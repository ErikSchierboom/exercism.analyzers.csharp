using System;
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
            Approved = false;
            ReferToMentor = false;
            Messages = Array.Empty<string>();
        }

        public TestSolutionAnalysisRun(int returnCode, AnalysisResult analysisResult)
        {
            Success = returnCode == 0;
            Approved = analysisResult.Approved;
            ReferToMentor = analysisResult.ReferredToMentor;
            Messages = analysisResult.Messages;
        }
    }
}