using Microsoft.Extensions.Logging;
using Transneft.Model.Interfaces;

namespace Transneft.Logic.Interfaces
{
    /// <summary>
    /// Интерфейс лога
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// Логгер
        /// </summary>
        ILogger Logger { get; set; }

        /// <summary>
        /// Записать лог
        /// </summary>
        /// <param name="msg">Сообщение</param>
        void Write(string msg);

        /// <summary>
        /// Записать лог из отклика
        /// </summary>
        /// <param name="resp">Отклик</param>
        void WriteResponse(IResponse resp);
    }
}
