﻿using System.Threading.Tasks;
using Exercism.Analyzers.CSharp.Analyzers;
using Serilog;

namespace Exercism.Analyzers.CSharp
{   
    public static class Analyzer
    {
        public static async Task<int> Analyze(string directory)
        {
            Log.Information("Analysing solution in {Directory}", directory);
            
            var solution = SolutionReader.Read(directory);
            if (solution.Track != Tracks.CSharp)
                return 1;

            var analysisResult = await SolutionAnalyzer.Analyze(solution);
            if (analysisResult == null)
                return 0;

            AnalysisResultWriter.Write(analysisResult);
            return 0;
        }
    }
}
