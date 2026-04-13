using System.ComponentModel.DataAnnotations;

namespace Moedelo.Changelog.Shared.Domain.Definitions.Documents
{
    public enum ItemType
    {
        [Display(Name = "Не указан")]
        Unknown = 0,

        [Display(Name = "Товар")]
        Goods = 1,

        [Display(Name = "Услуга")]
        Service = 2
    }
}