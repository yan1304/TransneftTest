using Transneft.WebApplication.Model.Enum;

namespace Transneft.WebApplication.Model
{
    /// <summary>
    /// Параметр для таблицы
    /// </summary>
    public class TableParam
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Тип устройства
        /// </summary>
        public TypeDevice Type { get; set; }
    }
}
