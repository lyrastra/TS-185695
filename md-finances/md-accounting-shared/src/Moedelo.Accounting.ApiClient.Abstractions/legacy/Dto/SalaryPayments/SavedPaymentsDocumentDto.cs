using System.Collections.Generic;
using Moedelo.Accounting.Enums.SalaryPayments;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.SalaryPayments
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
            IReadOnlyList<int> workerIds,
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

        public IReadOnlyList<int> WorkerIds { get; set; }

        public string PaymentNumber { get; set; }
    }
}