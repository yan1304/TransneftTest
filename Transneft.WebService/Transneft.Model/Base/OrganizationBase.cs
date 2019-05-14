using System.ComponentModel.DataAnnotations;

namespace Transneft.Model.Base
{
    /// <summary>
    /// Класс с полем "Адрес"
    /// </summary>
    public abstract class OrganizationBase : TransneftObject
    {
        /// <summary>
        /// Наименование
        /// </summary>
        [Required]
        [Display(Name = "Наименование")]
        public string Name { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        [Required]
        [Display(Name = "Адрес")]
        public string Address { get; set; }
    }
}
