using System.Collections.Generic;
using CSharpier.DocTypes;
using CSharpier.SyntaxPrinter;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpier.SyntaxPrinter.SyntaxNodePrinters
{
    public static class ParenthesizedLambdaExpression
    {
        public static Doc Print(ParenthesizedLambdaExpressionSyntax node)
        {
            // TODO 10 what about node.AttributeLists? it doesn't appear to be in the doc for c# 10
            // TODO 10 there is also the list of 5 things in #448 that probably don't require changes, but should be tested
            var docs = new List<Doc>
            {
                Modifiers.Print(node.Modifiers),
                ParameterList.Print(node.ParameterList),
                " ",
                Token.Print(node.ArrowToken)
            };
            if (node.ExpressionBody != null)
            {
                docs.Add(Doc.Group(Doc.Indent(Doc.Line, Node.Print(node.ExpressionBody))));
            }
            else if (node.Block != null)
            {
                if (node.Block.Statements.Count > 0)
                {
                    docs.Add(Doc.HardLine);
                }
                else
                {
                    docs.Add(" ");
                }

                docs.Add(Block.Print(node.Block));
            }

            return Doc.Concat(docs);
        }
    }
}
