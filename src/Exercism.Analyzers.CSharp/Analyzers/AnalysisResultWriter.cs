using System.IO;
using Newtonsoft.Json;

namespace Exercism.Analyzers.CSharp.Analyzers
{
    public static class AnalysisResultWriter
    {
        public static void Write(AnalysisResult analysisResult)
        {
            using (var fileWriter = File.CreateText(analysisResult.Solution.AnalysisFile()))
            using (var jsonTextWriter = new JsonTextWriter(fileWriter))
            {
                jsonTextWriter.WriteStartObject();
                jsonTextWriter.WritePropertyName("approve");
                jsonTextWriter.WriteValue(analysisResult.Approved);
                jsonTextWriter.WritePropertyName("refer_to_mentor");
                jsonTextWriter.WriteValue(analysisResult.ReferredToMentor);
                jsonTextWriter.WritePropertyName("messages");
                jsonTextWriter.WriteStartArray();
                jsonTextWriter.WriteValues(analysisResult.Messages);
                jsonTextWriter.WriteEndArray();
                jsonTextWriter.WriteEndObject();
            }
        }
    }
}