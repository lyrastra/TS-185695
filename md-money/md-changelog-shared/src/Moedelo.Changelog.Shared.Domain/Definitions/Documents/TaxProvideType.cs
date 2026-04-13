using System.ComponentModel.DataAnnotations;

namespace Moedelo.Changelog.Shared.Domain.Definitions.Documents
{
    public enum TaxProvideType
    {
        /// <summary>
        /// Автоматически
        /// </summary>
        [Display(Name = "Автоматически")]
        Auto = 0,
        
        /// <summary>
        /// Вручную
        /// </summary>
        [Display(Name = "Вручную")]
        ByHand = 1,
    }
}