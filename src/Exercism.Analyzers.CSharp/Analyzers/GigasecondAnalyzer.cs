using Exercism.Analyzers.CSharp.CodeAnalysis;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;
using Serilog;

namespace Exercism.Analyzers.CSharp.Analyzers
{
    public static class GigasecondAnalyzer
    {
        public static AnalysisResult Analyze(Solution solution, Compilation compilation)
        {
            Log.Information("Analysing {Exercise} using {Analyzer}", 
                solution.Exercise, nameof(GigasecondAnalyzer));

            var tree = compilation.GetImplementationSyntaxTree(solution);
            if (tree == null)
                return AnalysisResult.ReferToMentor(solution);

            var root = tree.GetRoot();
            var addMethod = root
                .GetClassDeclaration(solution.ExerciseFriendlyName())
                .GetMethodDeclaration("Add");
            
            if (addMethod == null)
                return AnalysisResult.ReferToMentor(solution);

            var operation =
                GetOperationFromBody(compilation, tree, addMethod) ??
                GetOperationFromExpressionBody(compilation, tree, addMethod);
            
            if (InvokedMethod(operation) == "System.DateTime.AddSeconds(double)")
                return AnalysisResult.Approve(solution);
                    
            if (InvokedMethod(operation) == "System.DateTime.Add(System.TimeSpan)")
                return AnalysisResult.ReferToStudent(solution, "Use AddSeconds");

            // invocationOperation.TargetMethod.ToDisplayString()
            // invocationOperation.OperatorMethod.ToDisplayString()
            if (InvokedMethod(operation) == "System.DateTime.operator +(System.DateTime, System.TimeSpan)")
                return AnalysisResult.ReferToStudent(solution, "Use AddSeconds");

            return AnalysisResult.ReferToMentor(solution);
        }

        private static IOperation GetOperationFromBody(Compilation compilation, SyntaxTree syntaxTree,
            MethodDeclarationSyntax addMethod)
        {
            if (addMethod.Body == null || addMethod.Body.Statements.Count != 1)
                return null;

            var semanticModel = compilation.GetSemanticModel(syntaxTree);
            var returnOperation = (IReturnOperation)semanticModel.GetOperation(addMethod.Body.Statements[0]);
            return returnOperation.ReturnedValue;
        }

        private static IOperation GetOperationFromExpressionBody(Compilation compilation, SyntaxTree syntaxTree,
            MethodDeclarationSyntax addMethod)
        {
            var semanticModel = compilation.GetSemanticModel(syntaxTree);
            return semanticModel.GetOperation(addMethod.ExpressionBody.Expression);
        }

        private static string InvokedMethod(IOperation operation) =>
            operation is IInvocationOperation invocationOperation
                ? invocationOperation.TargetMethod.ToDisplayString()
                : null;
        
//        public override void Initialize(AnalysisContext context)
//        {
//            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
//            context.EnableConcurrentExecution();
//            context.RegisterOperationAction(AnalyzeInvocation, OperationKind.Invocation);
//            context.RegisterOperationAction(AnalyzeBinaryOperator, OperationKind.BinaryOperator);
//        }
//
//        private static void AnalyzeInvocation(OperationAnalysisContext context)
//        {
//            if (context.SkipAnalysis())
//                return;
//
//            var invocationOperation = (IInvocationOperation)context.Operation;
//            var targetMethod = invocationOperation.TargetMethod.ToDisplayString();
//
//            if (targetMethod == "System.DateTime.Add(System.TimeSpan)")
//                context.ReportDiagnostic(Diagnostic.Create(Rule, invocationOperation.Syntax.GetLocation(), invocationOperation.Syntax.ToString()));
//        }
//
//        private static void AnalyzeBinaryOperator(OperationAnalysisContext context)
//        {
//            if (context.SkipAnalysis())
//                return;
//
//            var binaryOperation = (IBinaryOperation)context.Operation;
//            var targetMethod = binaryOperation.OperatorMethod.ToDisplayString();
//
//            if (targetMethod == "System.DateTime.operator +(System.DateTime, System.TimeSpan)")
//                context.ReportDiagnostic(Diagnostic.Create(Rule, binaryOperation.Syntax.GetLocation(), binaryOperation.Syntax.ToString()));
//        }

//private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(
//            id: DiagnosticIds.UseExponentNotationAnalyzerRuleId,
//            title: "Use exponent notation",
//            messageFormat: "You can write `{0}` as `{1}`.",
//            category: DiagnosticCategories.LanguageFeature,
//            defaultSeverity: DiagnosticSeverity.Info,
//            isEnabledByDefault: true);
//
//        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);
//
//        public override void Initialize(AnalysisContext context)
//        {
//            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
//            context.EnableConcurrentExecution();
//            context.RegisterOperationAction(AnalyzeNumericLiteralExpression, OperationKind.Literal);
//        }
//
//        private static void AnalyzeNumericLiteralExpression(OperationAnalysisContext context)
//        {
//            if (context.SkipAnalysis())
//                return;
//
//            var literalOperation = (ILiteralOperation)context.Operation;
//            if (!IsDouble(literalOperation) && !ImplicitlyConvertedToDouble(literalOperation))
//                return;
//
//            var literalExpression = (LiteralExpressionSyntax) literalOperation.Syntax;
//            if (!literalExpression.Token.IsKind(SyntaxKind.NumericLiteralToken))
//                return;
//
//            if (CouldUseExponentNotation(literalExpression))
//                context.ReportDiagnostic(
//                    Diagnostic.Create(
//                        Rule,
//                        literalExpression.GetLocation(),
//                        literalExpression.ToString(),
//                        SuggestedExpressionUsingExponentNotation(literalExpression.Token)));
//        }
//
//        private static bool IsDouble(IOperation literalOperation) =>
//            literalOperation.Type.SpecialType.Equals(SpecialType.System_Double);
//
//        private static bool ImplicitlyConvertedToDouble(IOperation literalOperation) =>
//            literalOperation.Parent is IConversionOperation conversionOperation &&
//            conversionOperation.IsImplicit &&
//            conversionOperation.Type.SpecialType.Equals(SpecialType.System_Double);
//
//        private static bool CouldUseExponentNotation(LiteralExpressionSyntax literalExpressionToken) =>
//            ValueWarrantsUsingExponentNotation(literalExpressionToken.Token) &&
//            !UsesExponentNotation(literalExpressionToken);
//
//        private static bool UsesExponentNotation(LiteralExpressionSyntax literalExpressionToken) =>
//            literalExpressionToken.Token.Text.Contains("e", StringComparison.OrdinalIgnoreCase);
//
//        private static bool ValueWarrantsUsingExponentNotation(in SyntaxToken literalExpressionToken) =>
//            literalExpressionToken.ValueText.Length > 5 && 
//            literalExpressionToken.ValueText.Skip(1).All(c => c == '0');
//
//        private static string SuggestedExpressionUsingExponentNotation(in SyntaxToken literalExpressionToken) =>
//            $"{literalExpressionToken.ValueText[0]}e{literalExpressionToken.ValueText.Length - 1}";

// private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(
//            id: DiagnosticIds.UseDigitSeparatorAnalyzerRuleId,
//            title: "Use digit separator",
//            messageFormat: "You can write `{0}` as `{1}`.",
//            category: DiagnosticCategories.LanguageFeature,
//            defaultSeverity: DiagnosticSeverity.Info,
//            isEnabledByDefault: true);
//
//        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);
//
//        public override void Initialize(AnalysisContext context)
//        {
//            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
//            context.EnableConcurrentExecution();
//            context.RegisterSyntaxNodeAction(AnalyzeNumericLiteralExpression, SyntaxKind.NumericLiteralExpression);
//        }
//
//        private static void AnalyzeNumericLiteralExpression(SyntaxNodeAnalysisContext context)
//        {
//            if (context.SkipAnalysis())
//                return;
//
//            var literalExpression = (LiteralExpressionSyntax)context.Node;
//            if (!literalExpression.Token.IsKind(SyntaxKind.NumericLiteralToken))
//                return;
//
//            if (CouldUseDigitSeparator(literalExpression.Token))
//                context.ReportDiagnostic(
//                    Diagnostic.Create(
//                        Rule,
//                        literalExpression.GetLocation(),
//                        literalExpression.ToString(),
//                        SuggestedExpressionUsingDigitSeparator(literalExpression.Token)));
//        }
//
//        private static bool CouldUseDigitSeparator(SyntaxToken literalExpressionToken) =>
//            ValueIsInteger(literalExpressionToken) &&
//            ValueNotDefinedUsingDigitSeparator(literalExpressionToken) &&
//            ValueWarrantsUsingDigitSeparator(literalExpressionToken);
//
//        private static bool ValueIsInteger(in SyntaxToken literalExpressionToken) =>
//            literalExpressionToken.Value is int;
//
//        private static bool ValueNotDefinedUsingDigitSeparator(SyntaxToken literalExpressionToken) =>
//            !literalExpressionToken.Text.Contains("_");
//
//        private static bool ValueWarrantsUsingDigitSeparator(SyntaxToken literalExpressionToken)
//        {
//            var value = (int) literalExpressionToken.Value;
//
//            if (IsBinaryNumber(literalExpressionToken))
//                return value > 0b1111;
//
//            if (IsHexadecimalNumber(literalExpressionToken))
//                return value > 0xFF;
//
//            return value > 99999;
//        }
//
//        private static bool IsBinaryNumber(SyntaxToken literalExpressionToken) =>
//            literalExpressionToken.Text.StartsWith("0b", StringComparison.OrdinalIgnoreCase);
//
//        private static bool IsHexadecimalNumber(SyntaxToken literalExpressionToken) =>
//            literalExpressionToken.Text.StartsWith("0x", StringComparison.OrdinalIgnoreCase);
//
//        private static string SuggestedExpressionUsingDigitSeparator(SyntaxToken literalExpressionToken)
//        {
//            var text = literalExpressionToken.Text.AsSpan();
//
//            if (IsBinaryNumber(literalExpressionToken))
//                return literalExpressionToken.Text.Substring(0, 2) + AddDigitSeparator(text.Slice(2), 4);
//
//            if (IsHexadecimalNumber(literalExpressionToken))
//                return literalExpressionToken.Text.Substring(0, 2) + AddDigitSeparator(text.Slice(2), 2);
//
//            return AddDigitSeparator(text, 3);
//        }
//
//        private static string AddDigitSeparator(ReadOnlySpan<char> literalExpression, int separatorWidth)
//        {
//            var literalExpressionWithSeparators = new Stack<char>();
//
//            for (var i = 0; i < literalExpression.Length; i++)
//            {
//                literalExpressionWithSeparators.Push(literalExpression[literalExpression.Length - 1 - i]);
//                if (i > 0 && i < literalExpression.Length - 1 && (i + 1) % separatorWidth == 0)
//                    literalExpressionWithSeparators.Push('_');
//            }
//
//            return new string(literalExpressionWithSeparators.ToArray());
//        }

//        private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(
//            id: DiagnosticIds.UseExpressionBodiedMemberAnalyzerRuleId,
//            title: "Use an expression-bodied member",
//            messageFormat: "The '{0}' method can be rewritten as an expression-bodied member.",
//            category: DiagnosticCategories.LanguageFeature,
//            defaultSeverity: DiagnosticSeverity.Info,
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
//            // TODO: add base class to help verify if the file being processed is not the test file
//            
//            if (MethodCanBeConvertedToExpressionBodiedMember(method))
//                context.ReportDiagnostic(Diagnostic.Create(Rule, method.GetLocation(), method.Identifier.ValueText));
//        }
//
//        private static bool MethodCanBeConvertedToExpressionBodiedMember(MethodDeclarationSyntax methodSyntax) =>
//            methodSyntax.ExpressionBody == null && methodSyntax.Body.Statements.Count == 1;
    }
}