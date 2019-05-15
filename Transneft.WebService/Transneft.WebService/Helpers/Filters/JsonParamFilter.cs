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
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult("Некорректный JSON-параметр");
            }

            await next.Invoke();
        }
    }
}
