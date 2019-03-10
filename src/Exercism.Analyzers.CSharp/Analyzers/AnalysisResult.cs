namespace Exercism.Analyzers.CSharp.Analyzers
{
    public class AnalysisResult
    {
        public Solution Solution { get; }
        public bool Approved { get; }
        public bool ReferredToMentor { get; }
        public string[] Messages { get; }

        public AnalysisResult(Solution solution, bool approved, bool referredToMentor, params string[] messages)
        {
            Solution = solution;
            Approved = approved;
            ReferredToMentor = referredToMentor;
            Messages = messages;
        }

        public static AnalysisResult Approve(Solution solution, params string[] messages) =>
            new AnalysisResult(solution, approved: true, referredToMentor: false, messages);

        public static AnalysisResult ReferToMentor(Solution solution, params string[] messages) =>
            new AnalysisResult(solution, approved: false, referredToMentor: true, messages);

        public static AnalysisResult ReferToStudent(Solution solution, params string[] messages) =>
            new AnalysisResult(solution, approved: false, referredToMentor: false, messages);
    }
}