using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Utils
{
    public static class HttpContextExtension
    {
        public async static Task InsertPaginationHeader<T>(this HttpContext httpContext, IQueryable<T> queryable)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));

            double count = await queryable.CountAsync();
            httpContext.Response.Headers.Add("QuantityTotalRecords", count.ToString());
        }
    }
}