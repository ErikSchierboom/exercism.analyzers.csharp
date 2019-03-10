using System.IO;
using Microsoft.CodeAnalysis;

namespace Exercism.Analyzers.CSharp.CodeAnalysis
{
    internal static class SyntaxTreeExtensions
    {
        public static string FileName(this SyntaxTree syntaxTree) => Path.GetFileName(syntaxTree.FilePath);
    }
}