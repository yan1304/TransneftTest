using Microsoft.AspNetCore.Mvc;

namespace Transneft.WebService.Controllers
{
    /// <summary>
    /// Стандартный контроллер
    /// </summary>
    [ApiController]
    [Route("")]
    public class DefaultController : ControllerBase
    {
        /// <summary>
        /// Использовать для проверки успешного соединения с сервисом
        /// </summary>
        /// <returns>Отклик</returns>
        [HttpGet]
        [Route("")]
        public string Index() => "OK";
    }
}
