using System.Linq;
using CSharpier.DocTypes;
using CSharpier.SyntaxPrinter;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpier.SyntaxPrinter.SyntaxNodePrinters
{
    public static class InitializerExpression
    {
        public static Doc Print(InitializerExpressionSyntax node)
        {
            // TODO clean up this code if we keep it
            // TODO handle this https://github.com/belav/csharpier-repos/pull/8/files#diff-21008119e248bd9f6fab1b1951077c06dfcbb1e311a364cf61375df336e54693
            Doc separator = node.Parent is AssignmentExpressionSyntax or EqualsValueClauseSyntax
                ? Doc.Line
                : Doc.Null;

            var alwaysBreak = node.Kind() == SyntaxKind.ObjectInitializerExpression && node.Expressions.Count >= 2;

            var result = Doc.Concat(
                separator,
                Token.Print(node.OpenBraceToken),
                Doc.Indent(
                    alwaysBreak ? Doc.HardLine : Doc.Line,
                    SeparatedSyntaxList.Print(node.Expressions, Node.Print, alwaysBreak ? Doc.HardLine : Doc.Line)
                ),
                alwaysBreak ? Doc.HardLine : Doc.Line,
                Token.Print(node.CloseBraceToken)
            );
            return node.Parent
                is not (
                    ObjectCreationExpressionSyntax
                    or ArrayCreationExpressionSyntax
                    or ImplicitArrayCreationExpressionSyntax
                    or ImplicitObjectCreationExpressionSyntax
                )
            && node.Kind() is not SyntaxKind.WithInitializerExpression
                ? Doc.Group(result)
                : result;
        }
    }
}
