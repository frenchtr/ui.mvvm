using System;
using System.Linq.Expressions;
using System.Reflection;

namespace TravisRFrench.UI.MVVM.DataBinding.Builders
{
	internal static class ExpressionUtil
	{
		public static string PropertyName<TObj, TValue>(Expression<Func<TObj, TValue>> expr)
		{
			if (expr == null) throw new ArgumentNullException(nameof(expr));

			Expression body = expr.Body;
			if (body is UnaryExpression u && u.NodeType == ExpressionType.Convert)
				body = u.Operand;

			if (body is not MemberExpression m || m.Member is not PropertyInfo pi)
				throw new ArgumentException("Expected a property expression like x => x.Property", nameof(expr));

			return pi.Name;
		}

		public static Action<TObj, TValue> Setter<TObj, TValue>(Expression<Func<TObj, TValue>> expr)
		{
			if (expr == null) throw new ArgumentNullException(nameof(expr));

			var obj = expr.Parameters[0];
			var value = Expression.Parameter(typeof(TValue), "value");

			Expression body = expr.Body;
			if (body is UnaryExpression u && u.NodeType == ExpressionType.Convert)
				body = u.Operand;

			if (body is not MemberExpression m || m.Member is not PropertyInfo pi || !pi.CanWrite)
				throw new ArgumentException("Expected a writable property expression like x => x.Property", nameof(expr));

			var assign = Expression.Assign(m, value);
			return Expression.Lambda<Action<TObj, TValue>>(assign, obj, value).Compile();
		}
	}
}