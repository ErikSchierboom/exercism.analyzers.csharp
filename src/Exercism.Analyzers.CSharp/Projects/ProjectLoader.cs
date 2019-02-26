using System.IO;
using Buildalyzer;
using Buildalyzer.Workspaces;
using Microsoft.CodeAnalysis;

namespace Exercism.Analyzers.CSharp.Projects
{
    internal static class ProjectLoader
    {
        public static Project LoadFromFile(FileInfo projectFile)
        {
            var project = new AnalyzerManager().GetProject(projectFile.FullName);
            return project.AddToWorkspace(new AdhocWorkspace());
        }
    }
}