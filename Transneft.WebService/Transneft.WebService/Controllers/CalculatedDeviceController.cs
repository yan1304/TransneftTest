using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Taransneft.Logic.Interfaces;

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
        public CalculatedDeviceController(ILogger logger, IQueryService service) : base(logger, service) { }

        /// <summary>
        /// GET CalculatedDevice (задание 1.2 п.2)
        /// Выбрать все расчетные приборы в 2018 году
        /// </summary>
        /// <returns>JSON-отклик</returns>
        [HttpGet("{year}")]
        [HttpGet()]
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
