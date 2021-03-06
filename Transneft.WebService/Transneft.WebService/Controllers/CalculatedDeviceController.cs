﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Taransneft.Logic.Interfaces;
using Transneft.Logic.Contexts;

namespace Transneft.WebService.Controllers
{
    /// <summary>
    /// Контроллер для работы с расчетными приборами учета
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CalculatedDeviceController : WebServiceControllerBase
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="logger">Логгер</param>
        /// <param name="service">Сервис для работы с БД</param>
        /// <param name="context">Контекст БД</param>
        public CalculatedDeviceController(ILogger<CalculatedDeviceController> logger, IQueryService service, TransneftDbContext context)
            : base(logger, service, context) { }

        /// <summary>
        /// GET CalculatedDevice (задание 1.2 п.2)
        /// Выбрать все расчетные приборы в 2018 году
        /// </summary>
        /// <returns>JSON-отклик</returns>
        [HttpGet]
        public IActionResult Get()
        {
            Log("Start GET CalculatedDevice");
            try
            {
                Log("Start QueryService.GetCalculatedDevices");
                var result = QueryService.GetCalculatedDevices(2018);
                Log("End QueryService.GetCalculatedDevices");
                Log("End GET CalculatedDevice");
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                Log($"Ошибка: {ex.Message}");
                return new BadRequestObjectResult($"Ошибка: {ex.Message}");
            }
        }
    }
}
