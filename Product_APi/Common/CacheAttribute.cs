using BLL.Cache;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics.CodeAnalysis;

namespace Product_APi.Common
{
    public class CacheAttribute : ActionFilterAttribute
    {
        //ICacheRepository cacheRepository = new CacheRepository(,);
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }
    }
}
