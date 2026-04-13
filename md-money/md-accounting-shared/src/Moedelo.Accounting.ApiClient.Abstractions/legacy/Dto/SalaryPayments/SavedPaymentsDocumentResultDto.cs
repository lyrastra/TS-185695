using System.Collections.Generic;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.SalaryPayments
{
    public class SavedPaymentsDocumentResultDto
    {
        public IReadOnlyList<SavedPaymentsDocumentDto> Documents { get; set; } = new List<SavedPaymentsDocumentDto>();

        /// <summary>
        /// Enum FileExtension
        /// </summary>
        public int Format { get; set; }
    }
}