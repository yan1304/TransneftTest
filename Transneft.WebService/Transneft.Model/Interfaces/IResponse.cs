namespace Transneft.Model.Interfaces
{
    /// <summary>
    /// Интерфейс отклика
    /// </summary>
    public interface IResponse
    {
        /// <summary>
        /// Успешно ли
        /// </summary>
        bool Success { get; set; }

        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        string ErrMsg { get; set; }

        /// <summary>
        /// JSON-данные
        /// </summary>
        string Data { get; set; }
    }
}
