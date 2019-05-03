using Transneft.Model.Interfaces;

namespace Transneft.Model.Interfaces
{
    /// <summary>
    /// Интерфейс лога
    /// </summary>
    public interface ILog
    {
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
