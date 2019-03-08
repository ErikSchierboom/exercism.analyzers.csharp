namespace Exercism.Analyzers.CSharp.Analyzers
{
    public readonly struct AnalysisResult
    {
        public readonly bool Approve;
        public readonly bool ReferToMentor;
        public readonly string[] Messages;

        public AnalysisResult(bool approve, bool referToMentor, string[] messages) =>
            (Approve, ReferToMentor, Messages) = (approve, referToMentor, messages);
    }
}