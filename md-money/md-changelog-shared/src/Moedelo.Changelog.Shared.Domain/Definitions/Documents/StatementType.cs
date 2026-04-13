using System.ComponentModel.DataAnnotations;

namespace Moedelo.Changelog.Shared.Domain.Definitions.Documents
{
    public enum StatementType
    {
        // Исходящий акт
        [Display(Name = "Прямая продажа")]
        Sale = 100,

        // Продажа по посредническому договору
        [Display(Name = "Продажа по посредническому договору")]
        SaleByMiddlemanContract = 110,

        // Входящий акт
        [Display(Name = "Покупка")]
        Buy = 200
    }
}
