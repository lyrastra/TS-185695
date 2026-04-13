using System.ComponentModel.DataAnnotations;

namespace Moedelo.Changelog.Shared.Domain.Definitions.Money.Common
{
    public class LinkedDocumentInfo
    {
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Сумма")]
        public MoneySum LinkSum { get; set; }
    }
}
