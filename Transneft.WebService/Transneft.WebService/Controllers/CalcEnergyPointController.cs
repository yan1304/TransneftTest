using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Taransneft.Logic.Interfaces;

namespace Transneft.WebService.Controllers
{
    /// <summary>
    /// Контроллер дляработы с точками измерения
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CalcEnergyPointController : WebServiceControllerBase
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="logger">Логгер</param>
        /// <param name="service">Сервис для работы с БД</param>
        public CalcEnergyPointController(ILogger logger, IQueryService service) : base(logger, service) { }

        /// <summary>
        /// POST CalcEnergyPoint (задание 1.2 п.1)
        /// Добавить новую точку измерения напряжения с указанием счётчика, трансформатора тока и трансформатора напряжения
        /// </summary>
        /// <param name="data">CalcEnergyPoint в json-формате</param>
        /// <returns>JSON-отклик</returns>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] object data)
        {
            Log("Start POST CalcEnergyPoint");
            try
            {
                var json = $"{data}";
                Log($"Param: {json}");
                Log("Start QueryService.AddCalcEnergyPoint");
                await QueryService.AddCalcEnergyPoint(json);
                Log("End QueryService.AddCalcEnergyPoint");
                Log("End POST CalcEnergyPoint");
                return new JsonResult("OK");

            }
            catch (Exception ex)
            {
                Log($"Ошибка: {ex.Message}");
                return new BadRequestObjectResult($"Ошибка: {ex.Message}");
            }
        }
    }
}
