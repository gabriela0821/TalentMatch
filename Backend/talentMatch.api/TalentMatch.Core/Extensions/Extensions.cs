using System.Linq.Expressions;

namespace TalentMatch.Core.Extensions
{
    public static class Extensions
    {
        public static IQueryable<T> OrderByPropertyOrField<T>(this IQueryable<T> queryable, string propertyOrFieldName, bool ascending)
        {
            Type typeFromHandle = typeof(T);
            string methodName = (ascending ? "OrderBy" : "OrderByDescending");
            ParameterExpression parameterExpression = Expression.Parameter(typeFromHandle);
            MemberExpression memberExpression = Expression.PropertyOrField(parameterExpression, propertyOrFieldName);
            LambdaExpression lambdaExpression = Expression.Lambda(memberExpression, parameterExpression);
            MethodCallExpression expression = Expression.Call(typeof(Queryable), methodName, new Type[2] { typeFromHandle, memberExpression.Type }, queryable.Expression, lambdaExpression);
            return queryable.Provider.CreateQuery<T>(expression);
        }
    }
}