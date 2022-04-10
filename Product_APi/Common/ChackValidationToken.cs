using BLL.Cache;
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
            string filterToken = string.Empty;
            for (int i = 7; i < token.Length; i++)
            {
                filterToken = filterToken + token[i];
            }
            var lendth = filterToken.Length;
            var blacest =_cacheRepository?.Get<Blacest>(filterToken);
            if ( blacest ==null )
                throw new Exception("paxar stexic aziz");
            if (!blacest.IsValid)
                throw new Exception("paxar stexic aziz");


            base.OnActionExecuting(context);
        }
    }
}
