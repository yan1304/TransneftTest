using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Taransneft.Logic.Interfaces;
using Transneft.Logic.Contexts;

namespace Transneft.WebService.Controllers
{
    /// <summary>
    /// Контроллер для работы с точками измерения
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
        /// <param name="context">Контекст БД</param>
        public CalcEnergyPointController(ILogger<CalcEnergyPointController> logger, IQueryService service, TransneftDbContext context) 
            : base(logger, service, context) { }

        /// <summary>
        /// POST CalcEnergyPoint (задание 1.2 п.1)
        /// Добавить новую точку измерения напряжения с указанием счётчика, трансформатора тока и трансформатора напряжения
        /// </summary>
        /// <param name="data">CalcEnergyPoint в json-формате</param>
        /// <param name="energyMeterId">Id счетчика электрической энергии</param>
        /// <param name="curTrId">Id трансформатора тока</param>
        /// <param name="voltTrId">Id трансформатора напряжения</param>
        /// <returns>JSON-отклик</returns>
        [HttpPost("{energyMeterId}/{curTrId}/{voltTrId}")]
        public async Task<IActionResult> Post([FromBody] object data, string energyMeterId, string curTrId, string voltTrId)
        {
            Log("Start POST CalcEnergyPoint");
            try
            {
                var json = $"{data}";
                Log($"Param: {json}");
                Log("Start QueryService.AddCalcEnergyPoint");
                await QueryService.AddCalcEnergyPoint(json, energyMeterId, curTrId, voltTrId);
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
