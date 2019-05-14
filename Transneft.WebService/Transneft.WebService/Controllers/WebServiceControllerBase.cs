using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Taransneft.Logic.Interfaces;
using Transneft.Core;
using Transneft.Logic;
using Transneft.Logic.Contexts;

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
        protected Log Logger { get; set; }

        /// <summary>
        /// Сервис для работы с запросами к БД
        /// </summary>
        protected IQueryService QueryService { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="logger">ILogger</param>
        /// <param name="service">Сервис для работы с запросами к БД</param>
        /// <param name="context">Контекст БД</param>
        public WebServiceControllerBase(ILogger<WebServiceControllerBase> logger, IQueryService service, TransneftDbContext context)
        {
            Logger = new Log(logger);
            QueryService = service;
            QueryService.Context = context;
            RequestId = Guid.NewGuid();
        }

        /// <summary>
        /// Записать лог контроллера
        /// </summary>
        /// <param name="msg">Сообщение</param>
        protected void Log(string msg) => Logger.Write($"RequestId = {RequestId}. {msg}");
    }
}
