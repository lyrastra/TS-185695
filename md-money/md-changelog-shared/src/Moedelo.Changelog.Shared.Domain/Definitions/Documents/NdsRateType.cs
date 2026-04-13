using System.ComponentModel.DataAnnotations;

namespace Moedelo.Changelog.Shared.Domain.Definitions.Documents
{
    public enum NdsRateType
    {
        [Display(Name = "")]
        UnknownNds = 0,

        /// <summary>
        /// Без НДС
        /// </summary>
        [Display(Name = "Без НДС")]
        WithoutNds = 1,

        /// <summary>
        /// НДС 0 %
        /// </summary>
        [Display(Name = "0 %")]
        Nds0 = 2,

        /// <summary>
        /// НДС 10 %
        /// </summary>
        [Display(Name = "10 %")]
        Nds10 = 3,

        /// <summary>
        /// НДС 18 %
        /// </summary>
        [Display(Name = "18 %")]
        Nds18 = 4,

        /// <summary>
        /// НДС 20 %
        /// </summary>
        [Display(Name = "20 %")]
        Nds20 = 5,

        /// <summary>
        /// НДС 5 %
        /// </summary>
        [Display(Name = "5 %")]
        Nds5 = 6,

        /// <summary>
        /// НДС 7 %
        /// </summary>
        [Display(Name = "7 %")]
        Nds7 = 7,

        /// <summary>
        /// НДС 22 %
        /// </summary>
        [Display(Name = "22 %")]
        Nds22 = 8
    }
}
