using System;
using System.Linq.Expressions;

namespace WPFCore
{
    public static class ExpressionHelper
    {
        public static string GetPropertyName<T, TProperty>(Expression<Func<T, TProperty>> expression)
        {
            var lambda = expression as LambdaExpression;
            if (lambda == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }
            var body = lambda.Body as UnaryExpression;
            var memberExpression = body != null
                ? body.Operand as MemberExpression
                : lambda.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException("Please provide a lambda expression like 'vm => vm.PropertyName'");
            }
            string propertyName = memberExpression.Member.Name;
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException("'expression' did not provide a property name.");
            }
            return propertyName;
        }
    }
}