using System.ComponentModel.DataAnnotations;

namespace Moedelo.Changelog.Shared.Domain.Definitions.Documents
{
    public enum UpdStatus
    {
        [Display(Name = "1 - счет-фактура и передаточный документ")]
        DocumentAndInvoice = 1,

        [Display(Name = "2 - передаточный документ")]
        Document = 2
    }
}