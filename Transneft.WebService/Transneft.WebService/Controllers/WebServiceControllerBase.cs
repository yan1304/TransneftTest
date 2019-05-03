using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
        protected ILogger Logger { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="logger"></param>
        public WebServiceControllerBase(ILogger logger) => Logger = logger;
    }
}
