using System;
using Exercism.Analyzers.CSharp.Analyzers;

namespace Exercism.Analyzers.CSharp.IntegrationTests.Helpers
{
    public class TestSolutionAnalysisRun
    {
        public bool Success { get; }
        public SolutionStatus Status { get; }
        public string[] Messages { get; }

        public TestSolutionAnalysisRun(int returnCode)
        {
            Success = returnCode == 0;
            Status = SolutionStatus.Unknown;
            Messages = Array.Empty<string>();
        }

        public TestSolutionAnalysisRun(int returnCode, AnalyzedSolution analyzedSolution)
        {
            Success = returnCode == 0;
            Status = analyzedSolution.Status;
            Messages = analyzedSolution.Messages;
        }
    }
}