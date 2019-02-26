﻿using System.Threading.Tasks;
using Exercism.Analyzers.CSharp.Solutions;

namespace Exercism.Analyzers.CSharp
{
    public static class Analyzer
    {
        public static async Task Analyze(string id)
        { 
            var loadedSolution = await LoadSolution(id);
            var compiledSolution = await CompileSolution(loadedSolution);
            var analysisResult = await AnalyzeSolution(compiledSolution);
            await WriteAnalysisResultToFile(analysisResult);
        }

        private static async Task<LoadedSolution> LoadSolution(string id)
        {
            var downloadedSolution = await SolutionDownloader.Download(id);
            return SolutionLoader.Load(downloadedSolution);
        }

        private static async Task<CompiledSolution> CompileSolution(LoadedSolution loadedSolution) =>
            await SolutionCompiler.Compile(loadedSolution);

        private static async Task<AnalysisResult> AnalyzeSolution(CompiledSolution compiledSolution)
        {
            var analyzedSolution = await SolutionAnalyzer.Analyze(compiledSolution);
            return new AnalysisResult(analyzedSolution.Status, analyzedSolution.Comments);
        }

        private static async Task WriteAnalysisResultToFile(AnalysisResult analysisResult)
        {
            // TODO: write to file
        }
    }
}
