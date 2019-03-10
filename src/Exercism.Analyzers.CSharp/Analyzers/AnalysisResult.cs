namespace Exercism.Analyzers.CSharp.Analyzers
{
    public class AnalysisResult
    {
        public Solution Solution { get; }
        public bool Approve { get; }
        public bool ReferToMentor { get; }
        public string[] Messages { get; }

        public AnalysisResult(Solution solution, bool approve, bool referToMentor, params string[] messages)
        {
            Solution = solution;
            Approve = approve;
            ReferToMentor = referToMentor;
            Messages = messages;
        }
    }
}