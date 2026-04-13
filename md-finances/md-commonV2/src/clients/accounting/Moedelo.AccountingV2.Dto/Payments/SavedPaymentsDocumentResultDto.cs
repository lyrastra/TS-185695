using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.Payments
{
    public class SavedPaymentsDocumentResultDto
    {
        public SavedPaymentsDocumentResultDto()
        {
            Documents = new List<SavedPaymentsDocumentDto>();
        }

        public List<SavedPaymentsDocumentDto> Documents { get; set; }

        /// <summary>
        /// Enum FileExtension
        /// </summary>
        public int Format { get; set; }
    }
}