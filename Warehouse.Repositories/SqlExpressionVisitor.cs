using System.Linq.Expressions;
using System.Text;

namespace Warehouse.Repositories;

public class SqlExpressionVisitor : ExpressionVisitor
{
    private StringBuilder _sql = new StringBuilder();
    private Dictionary<string, object> _parameters = new Dictionary<string, object>();
    private int _paramIndex = 0;

    public string Sql => _sql.ToString();
    public Dictionary<string, object> Parameters => _parameters;

    public static (string Sql, Dictionary<string, object> Parameters) ToSql<T>(Expression<Func<T, bool>> expression)
    {
        var visitor = new SqlExpressionVisitor();
        visitor.Visit(expression);
        return (visitor.Sql, visitor.Parameters);
    }

    protected override Expression VisitBinary(BinaryExpression node)
    {
        _sql.Append("(");

        Visit(node.Left);

        switch (node.NodeType)
        {
            case ExpressionType.Equal:
                _sql.Append(" = ");
                break;
            case ExpressionType.NotEqual:
                _sql.Append(" != ");
                break;
            case ExpressionType.GreaterThan:
                _sql.Append(" > ");
                break;
            case ExpressionType.GreaterThanOrEqual:
                _sql.Append(" >= ");
                break;
            case ExpressionType.LessThan:
                _sql.Append(" < ");
                break;
            case ExpressionType.LessThanOrEqual:
                _sql.Append(" <= ");
                break;
            case ExpressionType.AndAlso:
                _sql.Append(" AND ");
                break;
            case ExpressionType.OrElse:
                _sql.Append(" OR ");
                break;
            case ExpressionType.Add:
                _sql.Append(" + ");
                break;
            case ExpressionType.Subtract:
                _sql.Append(" - ");
                break;
            case ExpressionType.Multiply:
                _sql.Append(" * ");
                break;
            case ExpressionType.Divide:
                _sql.Append(" / ");
                break;
            default:
                throw new NotSupportedException($"Binary operator '{node.NodeType}' not supported");
        }

        Visit(node.Right);

        _sql.Append(")");

        return node;
    }

    protected override Expression VisitMember(MemberExpression node)
    {
        if (node.Expression != null && node.Expression.NodeType == ExpressionType.Parameter)
        {
            if (node.Type == typeof(bool))
                _sql.Append($"([{node.Member.Name}] = 1)");
            else
                _sql.Append($"[{node.Member.Name}]");
            return node;
        }

        object value = EvaluateMemberExpression(node);
        string paramName = $"@p{_paramIndex++}";
        _parameters.Add(paramName, value);
        _sql.Append(paramName);

        return node;
    }

    protected override Expression VisitConstant(ConstantExpression node)
    {
        string paramName = $"@p{_paramIndex++}";
        _parameters.Add(paramName, node.Value);
        _sql.Append(paramName);

        return node;
    }

    protected override Expression VisitMethodCall(MethodCallExpression node)
    {
        if (node.Method.Name == "Contains")
        {
            Visit(node.Object);
            _sql.Append(" LIKE ");
            string paramName = $"@p{_paramIndex++}";

            object value = Expression.Lambda(node.Arguments[0]).Compile().DynamicInvoke();
            _parameters.Add(paramName, $"%{value}%");
            _sql.Append(paramName);

            return node;
        }

        if (node.Method.Name == "StartsWith")
        {
            Visit(node.Object);
            _sql.Append(" LIKE ");
            string paramName = $"@p{_paramIndex++}";

            object value = Expression.Lambda(node.Arguments[0]).Compile().DynamicInvoke();
            _parameters.Add(paramName, $"{value}%");
            _sql.Append(paramName);

            return node;
        }

        if (node.Method.Name == "EndsWith")
        {
            Visit(node.Object);
            _sql.Append(" LIKE ");
            string paramName = $"@p{_paramIndex++}";

            object value = Expression.Lambda(node.Arguments[0]).Compile().DynamicInvoke();
            _parameters.Add(paramName, $"%{value}");
            _sql.Append(paramName);

            return node;
        }

        if (node.Method.Name == "Equals")
        {
            Visit(node.Object);
            _sql.Append(" = ");
            string paramName = $"@p{_paramIndex++}";
            object value = Expression.Lambda(node.Arguments[0]).Compile().DynamicInvoke();
            _parameters.Add(paramName, value);
            _sql.Append(paramName);
            return node;
        }

        throw new NotSupportedException($"Method '{node.Method.Name}' not supported");
    }

    private object EvaluateMemberExpression(MemberExpression node)
    {
        var lambda = Expression.Lambda(node).Compile();
        return lambda.DynamicInvoke();
    }
}
