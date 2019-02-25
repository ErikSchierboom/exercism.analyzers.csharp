using Microsoft.CodeAnalysis.Diagnostics;

namespace Exercism.Analyzers.CSharp.Analyzers.Leap
{
    internal static class LeapAnalyzers
    {
        public static readonly DiagnosticAnalyzer[] All = 
        {
            new UseMinimumNumberOfChecksForLeapYearAnalyzer()
        };
    }
}