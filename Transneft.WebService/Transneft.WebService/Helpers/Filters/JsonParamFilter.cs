using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using Transneft.Model;

namespace Transneft.WebService.Helpers
{
    /// <summary>
    /// Фильтр для проверки корректности JSON-параметра
    /// </summary>
    public class JsonParamFilter : IAsyncActionFilter
    {
        /// <summary>
        /// Фильтр для проверки корректности JSON-параметра
        /// </summary>
        /// <param name="context">ActionExecutingContext</param>
        /// <param name="next">ActionExecutingContext</param>
        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            var resultContext = await next();
            if (resultContext.HttpContext.Response.StatusCode == 502)
            {
                resultContext.Result = new JsonResult(new JsonResponse { ErrMsg = "Некорректный JSON-параметр" });
            }
        }
    }
}
