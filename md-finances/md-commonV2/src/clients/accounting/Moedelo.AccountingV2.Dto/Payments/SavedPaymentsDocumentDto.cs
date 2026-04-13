using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Payroll;

namespace Moedelo.AccountingV2.Dto.Payments
{
    public class SavedPaymentsDocumentDto
    {
        public SavedPaymentsDocumentDto()
        {
        }

        public SavedPaymentsDocumentDto(
            long id, 
            PaymentMethodType documentType, 
            WorkerPaymentType workerPaymentType, 
            List<int> workerIds, 
            string paymentNumber, 
            long? documentBaseId = null)
        {
            Id = id;
            DocumentBaseId = documentBaseId;
            DocumentType = documentType;
            WorkerPaymentType = workerPaymentType;
            WorkerIds = workerIds;
            PaymentNumber = paymentNumber;
        }

        public SavedPaymentsDocumentDto(long id, PaymentMethodType documentType)
        {
            Id = id;
            DocumentType = documentType;
        }

        public long Id { get; set; }

        public long? DocumentBaseId { get; set; }

        public PaymentMethodType DocumentType { get; set; }

        public WorkerPaymentType WorkerPaymentType { get; set; }

        public List<int> WorkerIds { get; set; }
        
        public string PaymentNumber { get; set; }
    }
}