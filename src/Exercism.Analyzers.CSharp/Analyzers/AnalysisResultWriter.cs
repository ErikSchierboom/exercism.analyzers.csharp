using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Exercism.Analyzers.CSharp.Analyzers
{
    internal static class JsonExtensions
    {
        public static void WriteValues<T>(this JsonTextWriter jsonTextWriter, IEnumerable<T> values)
        {
            foreach (var value in values)
                jsonTextWriter.WriteValue(value);
        }
    }
    
    public static class AnalysisResultWriter
    {
        private const string FileName = "analysis.json";

        public static void Write(AnalysisResult analysisResult)
        {
            using (var fileWriter = File.CreateText(Path.Combine(analysisResult.Solution.Directory, FileName)))
            using (var jsonTextWriter = new JsonTextWriter(fileWriter))
            {
                jsonTextWriter.WriteStartObject();
                jsonTextWriter.WritePropertyName("approve");
                jsonTextWriter.WriteValue(analysisResult.Approve);
                jsonTextWriter.WritePropertyName("refer_to_mentor");
                jsonTextWriter.WriteValue(analysisResult.ReferToMentor);
                jsonTextWriter.WritePropertyName("messages");
                jsonTextWriter.WriteStartArray();
                jsonTextWriter.WriteValues(analysisResult.Messages);
                jsonTextWriter.WriteEndArray();
                jsonTextWriter.WriteEndObject();
            }
        }
    }
}