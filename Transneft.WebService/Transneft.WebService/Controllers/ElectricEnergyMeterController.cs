using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Taransneft.Logic.Interfaces;
using Transneft.Logic.Contexts;

namespace Transneft.WebService.Controllers
{
    /// <summary>
    /// Контроллер для работы с счетчиками электрической энергии
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ElectricEnergyMeterController : WebServiceControllerBase
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="logger">Логгер</param>
        /// <param name="service">Сервис для работы с БД</param>
        /// <param name="context">Контекст БД</param>
        public ElectricEnergyMeterController(ILogger<ElectricEnergyMeterController> logger, IQueryService service, TransneftDbContext context)
            : base(logger, service, context) { }

        /// <summary>
        /// GET ElectricEnergyMeter(id) (задание 1.2 п.3)
        /// По указанному объекту потребления выбрать все счетчики с закончишившимся сроком проверки
        /// </summary>
        /// <param name="id">Id объекта потребления</param>
        /// <returns>JSON-отклик</returns>
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

        /// <summary>
        /// GET ElectricEnergyMeter
        /// Выбрать все не используемые счетчики
        /// </summary>
        /// <returns>JSON-отклик</returns>
        [HttpGet]
        public IActionResult Get()
        {
            Log("Start GET ElectricEnergyMeter (all)");
            try
            {
                Log("Start QueryService.GetDisabledElectricEnergyMeters");
                var result = QueryService.GetDisabledElectricEnergyMeters();
                Log("End QueryService.GetDisabledElectricEnergyMeters");
                Log("End GET ElectricEnergyMeter (all)");
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
