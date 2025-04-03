using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Scf.Servico.Domain.Util
{
 
    [ExcludeFromCodeCoverageAttribute]
    public static class ExpressionExtensions
    {
        public static Expression Simplify(this Expression expression)
        {
            var searcher = new ParameterlessExpressionSearcher();

            searcher.Visit(expression);

            return new ParameterlessExpressionEvaluator(searcher.ParameterlessExpressions).Visit(expression);
        }

        public static Expression<T> Simplify<T>(this Expression<T> expression) =>
           (Expression<T>)Simplify((Expression)expression);
    }

    [ExcludeFromCodeCoverageAttribute]
    public class ParameterlessExpressionSearcher : ExpressionVisitor
    {
        public HashSet<Expression> ParameterlessExpressions { get; } = new HashSet<Expression>();
        private bool containsParameter = false;

        public override Expression? Visit(Expression? node)
        {
            bool originalContainsParameter = containsParameter;

            containsParameter = false;

            base.Visit(node);

            if (!containsParameter)
            {
                if (node?.NodeType == ExpressionType.Parameter)
                    containsParameter = true;
                else
                    ParameterlessExpressions.Add(node!);
            }

            containsParameter = containsParameter || originalContainsParameter;

            return node;
        }
    }

    [ExcludeFromCodeCoverageAttribute]
    public class ParameterlessExpressionEvaluator : ExpressionVisitor
    {
        private readonly HashSet<Expression> parameterlessExpressions;

        public ParameterlessExpressionEvaluator(HashSet<Expression> parameterlessExpressions)
        {
            this.parameterlessExpressions = parameterlessExpressions;
        }

        public override Expression Visit(Expression? node)
        {
            if (node is null)
                throw new ArgumentNullException(nameof(node));

            if (parameterlessExpressions.Contains(node))
                return Evaluate(node);
            else
                return base.Visit(node);
        }

        private static Expression Evaluate(Expression node)
        {
            if (node.NodeType == ExpressionType.Constant)
                return node;

            object? value = Expression.Lambda(node).Compile().DynamicInvoke();

            return Expression.Constant(value, node.Type);
        }
    }
}
