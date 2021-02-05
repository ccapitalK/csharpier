using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpier
{
    public partial class Printer
    {
        private Doc PrintParenthesizedLambdaExpressionSyntax(ParenthesizedLambdaExpressionSyntax node)
        {
            var parts = new Parts();
            if (NotNullToken(node.AsyncKeyword)) {
                parts.Add(String("async "));
            }
            parts.Push(
                this.PrintParameterListSyntax(node.ParameterList),
                String(" => ")
            );
            if (node.ExpressionBody != null) {
                parts.Add(this.Print(node.ExpressionBody));
            } else if (node.Block != null) {
                parts.Add(this.PrintBlockSyntax(node.Block));
            }
            return Concat(parts);
        }
    }
}
