using System.ComponentModel.DataAnnotations;

namespace Moedelo.Changelog.Shared.Domain.Definitions.Documents
{
    public enum WaybillType
    {
        // Продажи
        [Display(Name = "Продажа")]
        Sale = 200,

        [Display(Name = "Безвозмездная передача")]
        SaleDonation = 201,

        [Display(Name = "Возврат поставщику")]
        ReturnToSupplier = 103,
        
        // Покупки
        [Display(Name = "Покупка")]
        Buy = 100,
        
        [Display(Name = "Безвозмездное получение")]
        BuyDonation = 101,
        
        [Display(Name = "По посредническому договору")]
        AgentIncoming = 104
    }
}