using Microsoft.CodeAnalysis;

namespace Exercism.Analyzers.CSharp.Solutions
{
    public class CompiledSolution
    {
        public Solution Solution { get; }
        public Compilation Compilation { get; }

        public CompiledSolution(in Solution solution, Compilation compilation) =>
            (Solution, Compilation) = (solution, compilation);
    }
}