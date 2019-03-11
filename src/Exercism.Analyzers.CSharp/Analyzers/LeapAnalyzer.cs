using Serilog;
using static Exercism.Analyzers.CSharp.Analyzers.LeapSolutions;

namespace Exercism.Analyzers.CSharp.Analyzers
{
    public static class LeapAnalyzer
    {
        public static AnalyzedSolution Analyze(Solution solution)
        {
            Log.Information("Analysing {Exercise} using {Analyzer}", 
                solution.Exercise, nameof(LeapAnalyzer));

            if (solution.ImplementationIsEquivalentTo(MinimumNumberOfChecks))
                return new AnalyzedSolution(solution, SolutionStatus.Approve);

            if (solution.ImplementationIsEquivalentTo(UnneededParentheses))
                return new AnalyzedSolution(solution, SolutionStatus.Approve);

            if (solution.ImplementationIsEquivalentTo(MethodWithBlockBody))
                return new AnalyzedSolution(solution, SolutionStatus.Approve, "You could write the method an an expression-bodied member");

            if (solution.UsesTooManyChecks())
                return new AnalyzedSolution(solution, SolutionStatus.ReferToMentor, "Use minimum number of checks");

            return new AnalyzedSolution(solution, SolutionStatus.ReferToMentor);
        }

        private static bool UsesTooManyChecks(this Solution solution)
        {
            // TODO: add checks
            
            return false;
//            private const int MinimalNumberOfChecks = 3;
//        private const string LeapClassIdentifier = "Leap";
//        private const string IsLeapYearMethodIdentifier = "IsLeapYear";
//        private const string YearParameterIdentifier = "year";
//        
//        private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(
//            id: DiagnosticIds.UseMinimumNumberOfChecksForLeapYearAnalyzerRuleId,
//            title: "Leap does not use minimum number of checks",
//            messageFormat: $"The '{IsLeapYearMethodIdentifier}' method uses too many checks.",
//            category: DiagnosticCategories.Optimization,
//            defaultSeverity: DiagnosticSeverity.Error,
//            isEnabledByDefault: true);
//
//        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);
//
//        public override void Initialize(AnalysisContext context)
//        {
//            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
//            context.EnableConcurrentExecution();
//            context.RegisterSyntaxNodeAction(AnalyzeMethodDeclaration, SyntaxKind.MethodDeclaration);
//        }
//
//        private static void AnalyzeMethodDeclaration(SyntaxNodeAnalysisContext context)
//        {
//            if (context.SkipAnalysis())
//                return;
//            
//            var method = (MethodDeclarationSyntax)context.Node;
//
//            if (IsLeapYearMethod(method) && UsesTooManyChecks(method))
//                context.ReportDiagnostic(Diagnostic.Create(Rule, method.GetLocation()));
//        }
//
//        private static bool IsLeapYearMethod(MethodDeclarationSyntax method) =>
//            method.Identifier.Text == IsLeapYearMethodIdentifier && 
//            method.Parent is ClassDeclarationSyntax classSyntax && 
//            classSyntax.Identifier.Text == LeapClassIdentifier;
//
//        private static bool UsesTooManyChecks(MethodDeclarationSyntax method) =>
//            method
//               .DescendantNodes()
//               .OfType<BinaryExpressionSyntax>()
//               .Count(BinaryExpressionUsesYearParameter) > MinimalNumberOfChecks;
//
//        private static bool BinaryExpressionUsesYearParameter(BinaryExpressionSyntax binaryExpression) =>
//            ExpressionUsesYearParameter(binaryExpression.Left) ||
//            ExpressionUsesYearParameter(binaryExpression.Right);
//
//        private static bool ExpressionUsesYearParameter(ExpressionSyntax expression) =>
//            expression is IdentifierNameSyntax nameSyntax &&
//            nameSyntax.Identifier.Text == YearParameterIdentifier;
        }
    }
}