using BLL.Cache;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Product_APi.Blacist;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Product_APi.Common
{
    
    public class ChackValidationTokenAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        
        {
            var _cacheRepository  = context.HttpContext.RequestServices.GetRequiredService<ICacheRepository>();

            var tokenPeir = context.HttpContext.Request.Headers?.FirstOrDefault(x=>x.Key== "Authorization");
            var token= tokenPeir.Value.Value.FirstOrDefault();
            if (string.IsNullOrEmpty(token))
            {
                context.Result = new BadRequestObjectResult("wrong token");
                return;
            }
            token = token.Replace("Bearer ", "");
            var blacest =_cacheRepository?.Get<BlackList>(token);
            if (blacest == null || !blacest.IsValid)
            {
                context.Result = new BadRequestObjectResult("wrong token");
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
