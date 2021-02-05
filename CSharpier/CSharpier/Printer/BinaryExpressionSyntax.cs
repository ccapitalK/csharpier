using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpier
{
    public partial class Printer
    {
        private Doc PrintBinaryExpressionSyntax(BinaryExpressionSyntax node)
        {
            return this.PrintLeftRightOperator(node, node.Left, node.OperatorToken, node.Right);
        }
    }
}
