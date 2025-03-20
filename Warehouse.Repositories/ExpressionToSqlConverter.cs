using System.Linq.Expressions;

namespace Warehouse.Repositories;
internal class ExpressionToSqlConverter
{
    public static string ConvertExpressionToSql<T>(Expression<Func<T, bool>> expression)
    {
        Expression body = expression.Body;
        if(body is BinaryExpression binaryExpression)
            return GetSql(binaryExpression);
        if(body is MemberExpression memberExpression)
            return $"{memberExpression.Member.Name} = 1";
        return "";
    }

    private static string GetSql(BinaryExpression expression)
    {
        string left = expression.Left is BinaryExpression ?
            $"({GetSql((BinaryExpression)expression.Left)})" : $"{GetName(expression.Left)}";
        string right = expression.Right is BinaryExpression ?
            $"({GetSql((BinaryExpression)expression.Right)})" : $"{GetName(expression.Right)}";

        return $"{left} {GetSqlOperator(expression.NodeType)} {right}";
    }

    public static string GetName(Expression expression)
    {
        if (expression is MemberExpression memberExpression)
            return memberExpression.Member.Name;

        if (expression is ConstantExpression constantExpression)
            return ConstantExpressionToString(constantExpression);

        if (expression is UnaryExpression unaryExpression) {
            return GetName(unaryExpression.Operand);
        }

        throw new NotImplementedException();
    }

    private static string ConstantExpressionToString(ConstantExpression constantExpression)
    {
        object? value = constantExpression.Value;

        if (value is bool boolean)
            return boolean ? "1" : "0";

        if (value is string text)
            return $"'{text}'";

        return value.ToString();
    }

    private static string GetSqlOperator(ExpressionType type)
    {
        return type switch
        {
            ExpressionType.AndAlso => "AND",
            ExpressionType.Or => "OR",
            ExpressionType.Equal => "=",
            ExpressionType.NotEqual => "!=",
            ExpressionType.GreaterThan => ">",
            ExpressionType.GreaterThanOrEqual => ">=",
            ExpressionType.LessThan => "<",
            ExpressionType.LessThanOrEqual => "<=",
            _ => throw new NotSupportedException($"Unsupported operator: {type}")
        };
    }
}
