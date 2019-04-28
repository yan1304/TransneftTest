namespace Transneft.Model.Base
{
    /// <summary>
    /// Класс с полем "Адрес"
    /// </summary>
    public abstract class AddressObject : TransneftObject
    {
        /// <summary>
        /// Адрес
        /// </summary>
        public string Adress { get; set; }
    }
}
