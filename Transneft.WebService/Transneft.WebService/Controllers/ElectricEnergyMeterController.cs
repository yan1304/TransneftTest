﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Taransneft.Logic.Interfaces;

namespace Transneft.WebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ElectricEnergyMeterController : WebServiceControllerBase
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="logger">Логгер</param>
        /// <param name="service">Сервис для работы с БД</param>
        public ElectricEnergyMeterController(ILogger logger, IQueryService service) : base(logger, service) { }
    }
}
