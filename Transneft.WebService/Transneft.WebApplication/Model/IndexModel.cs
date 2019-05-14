using System.Collections.Generic;
using Transneft.Model;

namespace Transneft.WebApplication.Model
{
    /// <summary>
    /// Модель для главного окна веб-приложения
    /// </summary>
    public class IndexModel
    {
        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Объекты потребления
        /// </summary>
        public IEnumerable<ItemInfo> ConsObjects { get; set; }
    }
}
