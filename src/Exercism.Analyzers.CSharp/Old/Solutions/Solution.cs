namespace Exercism.Analyzers.CSharp.Solutions
{
    public readonly struct Solution
    {
        public readonly string Id;
        public readonly Exercise Exercise;

        public Solution(string id, in Exercise exercise) => (Id, Exercise) = (id, exercise);
    }
}