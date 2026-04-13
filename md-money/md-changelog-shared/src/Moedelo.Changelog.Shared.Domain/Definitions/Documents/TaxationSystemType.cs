using System.ComponentModel.DataAnnotations;

namespace Moedelo.Changelog.Shared.Domain.Definitions.Documents
{
    public enum TaxationSystemType
    {
        [Display(Name = "УСН")]
        Usn = 1,

        [Display(Name = "ОСНО")]
        Osno = 2,

        [Display(Name = "ЕНВД")]
        Envd = 3,
        
        [Display(Name = "УСН + ЕНВД")]
        UsnAndEnvd = 4,

        [Display(Name = "ОСНО + ЕНВД")]
        OsnoAndEnvd = 5,
        
        [Display(Name = "Патент")]
        Patent = 6
    }
}