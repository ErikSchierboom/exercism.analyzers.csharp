using System.IO;
using Exercism.Analyzers.CSharp.Analyzers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Exercism.Analyzers.CSharp.IntegrationTests.Helpers
{
    public static class AnalyisResultReader
    {
        public static AnalysisResult Read(Solution solution)
        {
            using (var fileReader = File.OpenText(solution.AnalysisFile()))
            using (var jsonReader = new JsonTextReader(fileReader))
            {
                var jsonAnalysisResult = JToken.ReadFrom(jsonReader);

                // We read each JSON property by key to verify that the format is correct.
                // Automatically deserializing could possible use different keys. 
                var approve = jsonAnalysisResult["approve"].ToObject<bool>();
                var referToMentor = jsonAnalysisResult["refer_to_mentor"].ToObject<bool>();
                var messages = jsonAnalysisResult["messages"].ToObject<string[]>();
            
                return new AnalysisResult(solution, approve, referToMentor, messages);
            }
        }
    }
}