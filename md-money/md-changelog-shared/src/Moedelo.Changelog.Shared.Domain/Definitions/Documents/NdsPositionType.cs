using System.ComponentModel.DataAnnotations;

namespace Moedelo.Changelog.Shared.Domain.Definitions.Documents
{
    public enum NdsPositionType
    {
        [Display(Name = "Без НДС")]
        None = 1,

        [Display(Name = "Сверху")]
        Top = 2,

        [Display(Name = "В том числе")]
        Inside = 3
    }
}
