namespace AnyReference
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class Ref
    {
        public static Ref<T> Of<T>(Expression<Func<T>> expr)
        {
            var get = expr.Compile();
            var param = new[] { Expression.Parameter(typeof(T)) };
            var op = CreateSetOperation((dynamic) expr.Body, param[0]);
            var act = Expression.Lambda<Action<T>>(op, param);
            var set = act.Compile();
            return new Ref<T>(get, set);
        }

        internal static Expression CreateSetOperation(Expression expr, Expression param)
        {
            throw new NotSupportedException("This kind of expression is not supported for assignment.");
        }

        internal static Expression CreateSetOperation(BinaryExpression expr, Expression param)
        {
            var access = Expression.ArrayAccess(expr.Left, expr.Right);
            return Expression.Assign(access, param );
        }

        internal static Expression CreateSetOperation(MemberExpression expr, Expression param)
        {
            var propertyInfo = expr.Member as PropertyInfo;
            if (propertyInfo != null && !propertyInfo.CanWrite)
            {
                return Expression.Throw(Expression.New(typeof(InvalidOperationException)));
            }

            return Expression.Assign(expr, param);
        }

        internal static Expression CreateSetOperation(MethodCallExpression expr, Expression param)
        {
            var setterName = expr.Method.Name.Replace("get_", "set_");
            var setter = expr.Method.DeclaringType?.GetMethod(setterName);
            if (setter == null)
            {
                return Expression.Throw(Expression.New(typeof(InvalidOperationException)));
            }
            else
            {
                return Expression.Call(expr.Object, setter, expr.Arguments.Concat(new[] { param }));
            }
        }
    }
}
