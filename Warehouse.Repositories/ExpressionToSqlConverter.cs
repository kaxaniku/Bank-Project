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
        if (body is MethodCallExpression methodCallExpression)
            return MethodToSql(methodCallExpression);
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

        if (expression is MethodCallExpression methodCallExpression)
        {
            return MethodToSql(methodCallExpression);
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

    private static string MethodToSql(MethodCallExpression methodCallExpression)
    {
        if (methodCallExpression.Method.Name == "StartsWith")
        {
            string sqlString = null!;
            sqlString += GetName(methodCallExpression.Object!);
            sqlString += " LIKE ";
            sqlString += $"'{Expression.Lambda<Func<string>>(methodCallExpression.Arguments[0]).Compile().Invoke()}%'";
            return sqlString;
        }

        if (methodCallExpression.Method.Name == "Equals")
        {
            string sqlString = null!;
            sqlString += GetName(methodCallExpression.Object!);
            sqlString += " = ";
            if (methodCallExpression.Arguments[0].Type == typeof(int))
            {
                sqlString += $"{Expression.Lambda<Func<int>>(methodCallExpression.Arguments[0]).Compile().Invoke()}";
            }
            else if(methodCallExpression.Arguments[0].Type == typeof(string))
            {
                sqlString += $"'{Expression.Lambda<Func<string>>(methodCallExpression.Arguments[0]).Compile().Invoke()}'";
            } else
            {
                throw new NotImplementedException();
            }
                return sqlString;
        }

        throw new NotImplementedException();
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
