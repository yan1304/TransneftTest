using Microsoft.Extensions.Logging;

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
    }
}
