using BLL.Cache;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Product_APi.Common
{
    public class CacheAttribute : ActionFilterAttribute
    {
        //ICacheRepository cacheRepository = new CacheRepository(,);
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //var tt = context.HttpContext.RequestServices.GetRequiredService<>
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheRepository>();

            // var casheKey = GenerateCachKeyfromRequest(context.HttpContext.Request);
            var cachedResponse = cacheService.GetCacheResponse();

        }

        //private string GenerateCachKeyfromRequest(HttpRequest request)
        //{
        //    var keyBuilder = new StringBuilder();
        //    keyBuilder.Append($"{request.Path}")
        //}
    }
}
