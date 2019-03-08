using System;
using Microsoft.CodeAnalysis;

namespace Exercism.Analyzers.CSharp.Analyzers
{
    public static class GigasecondAnalyzer
    {
        public static AnalysisResult Analyze(Compilation compilation)
        {
            return new AnalysisResult(approve: false, referToMentor: true, messages: Array.Empty<string>());
        }
    }
}