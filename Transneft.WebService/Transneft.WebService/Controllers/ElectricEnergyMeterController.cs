using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            Log("Start GET ElectricEnergyMeter");
            try
            {
                Log("Start QueryService.GetDeadlinedEnergyMeters");
                var result = QueryService.GetDeadlinedEnergyMeters(id);
                Log("End QueryService.GetDeadlinedEnergyMeters");
                Log("End GET ElectricEnergyMeter");
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
