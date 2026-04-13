using System.ComponentModel.DataAnnotations;

namespace Moedelo.Changelog.Shared.Domain.Definitions.Documents
{
    public enum SaleUpdType
    {
        [Display(Name = "Продажа")]
        Sale = 0,

        [Display(Name = "Перемещение на склад комиссионера")]
        TransferToAgentStock = 1
    }
}