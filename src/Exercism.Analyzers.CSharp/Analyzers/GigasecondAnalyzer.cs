using System;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;

namespace Exercism.Analyzers.CSharp.Analyzers
{
    public static class GigasecondAnalyzer
    {
        public static Task<AnalysisResult> Analyze(Solution solution, Compilation compilation)
        {
            
            return Task.FromResult(new AnalysisResult(solution, approve: false, referToMentor: true, messages: Array.Empty<string>()));
        }
    }
}