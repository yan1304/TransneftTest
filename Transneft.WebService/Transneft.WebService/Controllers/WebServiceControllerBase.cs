using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Taransneft.Logic.Interfaces;
using Transneft.Logic.Interfaces;

namespace Transneft.WebService.Controllers
{
    /// <summary>
    /// Базовый класс контроллера
    /// </summary>
    public class WebServiceControllerBase : ControllerBase
    {
        /// <summary>
        /// Id запроса
        /// </summary>
        protected Guid RequestId { get; set; }

        /// <summary>
        /// Логгер 
        /// </summary>
        protected ILog Logger { get; set; }

        /// <summary>
        /// Сервис для работы с запросами к БД
        /// </summary>
        protected IQueryService QueryService { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="logger">ILogger</param>
        /// <param name="service">Сервис для работы с запросами к БД</param>
        public WebServiceControllerBase(ILogger logger, IQueryService service)
        {
            Logger = HttpContext.RequestServices.GetRequiredService<ILog>();
            Logger.Logger = logger;
            QueryService = service;
            RequestId = Guid.NewGuid();
        }

        /// <summary>
        /// Записать лог контроллера
        /// </summary>
        /// <param name="msg">Сообщение</param>
        protected void Log(string msg) => Logger.Write($"RequestId = {RequestId}. {msg}");
    }
}
