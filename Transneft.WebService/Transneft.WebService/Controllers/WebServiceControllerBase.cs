using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Transneft.Model;
using Transneft.Logic.Interfaces;

namespace Transneft.WebService.Controllers
{
    /// <summary>
    /// Базовый класс контроллера
    /// </summary>
    public class WebServiceControllerBase : ControllerBase
    {
        /// <summary>
        /// Логгер 
        /// </summary>
        protected ILog Logger { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="logger">ILogger</param>
        public WebServiceControllerBase(ILogger logger)
        {
            Logger = HttpContext.RequestServices.GetRequiredService<ILog>();
            Logger.Logger = logger;
        }

        /// <summary>
        /// Залогировать отклик и вернуть JsonResult
        /// </summary>
        /// <param name="log">Логгер</param>
        /// <param name="resp">Отклик</param>
        /// <returns>JsonResult</returns>
        protected JsonResult WriteLogAndReturn(JsonResponse resp)
        {
            Logger.WriteResponse(resp);
            return new JsonResult(resp);
        }
    }
}
