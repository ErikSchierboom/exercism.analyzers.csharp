using System.Threading.Tasks;

namespace Exercism.Analyzers.CSharp.IntegrationTests
{
    public static class TestSolutionAnalyzer
    {
        public static async Task<TestSolutionAnalysisRun> Run(string exercise, string code) =>
            await Run(new TestSolution(exercise), code);

        public static async Task<TestSolutionAnalysisRun> Run(TestSolution testSolution, string code)
        {
            testSolution.CreateFiles(code);

            var returnCode = await Program.Main(new[] {testSolution.Directory});
            if (returnCode > 0)
                return new TestSolutionAnalysisRun(returnCode);

            var analysisResult = AnalyisResultReader.Read(testSolution);
            return new TestSolutionAnalysisRun(returnCode, analysisResult);
        }
    }
}