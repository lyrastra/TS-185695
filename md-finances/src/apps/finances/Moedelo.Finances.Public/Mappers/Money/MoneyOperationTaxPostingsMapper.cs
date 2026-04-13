using System.Collections.Generic;
using System.Linq;
using Moedelo.Common.Enums.Enums.PostingEngine;
using Moedelo.Common.Enums.Enums.TaxPostings;
using Moedelo.Finances.Domain.Models.Money.Operations.TaxPostings;
using Moedelo.Finances.Public.ClientData.Money.Operations.TaxPostings;

namespace Moedelo.Finances.Public.Mappers.Money
{
    public static class MoneyOperationTaxPostingsMapper
    {
        public static TaxPostingsResponseClientData MapTaxPostingsResponse(TaxPostingList taxPostingList)
        {
            return new TaxPostingsResponseClientData
            {
                Operations = new List<TaxPostingsClientData>() { MapTaxPostings(taxPostingList) },
                ExplainingMessage = GetExplainingMessage(taxPostingList)
            };
        }

        private static TaxPostingsClientData MapTaxPostings(TaxPostingList taxPostingList)
        {
            return new TaxPostingsClientData
            {
                IsManualEdit = taxPostingList.IsManual,
                OperationType = taxPostingList.OperationType,
                Postings = taxPostingList.Postings.Select(MapTaxPostingDescription).ToList(),
                LinkedDocuments = taxPostingList.LinkedDocuments.Select(MapTaxPostingLinkedDocument).ToList(),
            };
        }

        private static TaxPostingLinkedDocumentClientData MapTaxPostingLinkedDocument(TaxPostingLinkedDocument linkedDocument)
        {
            return new TaxPostingLinkedDocumentClientData
            {
                DocumentName = linkedDocument.DocumentName,
                DocumentNumber = linkedDocument.DocumentNumber,
                DocumentDate = linkedDocument.DocumentDate,
                Type = linkedDocument.Type,
                Postings = linkedDocument.Postings.Select(MapTaxPostingDescription).ToList(),
            };
        }

        private static TaxPostingDescriptionClientData MapTaxPostingDescription(TaxPosting postingDescription)
        {
            return new TaxPostingDescriptionClientData
            {
                PostingDate = postingDescription.Date,
                Incoming = postingDescription.Direction == TaxPostingsDirection.Incoming
                    ? postingDescription.Sum
                    : 0m,
                Outgoing = postingDescription.Direction == TaxPostingsDirection.Outgoing
                    ? postingDescription.Sum
                    : 0m,
                Destination = postingDescription.Description,
                Type = postingDescription.Type,
                Kind = postingDescription.Kind,
                NormalizedCostType = postingDescription.NormalizedCostType,
            };
        }

        private static string GetExplainingMessage(TaxPostingList taxPostingList)
        {
            const string NonTaxableMessage = "Не учитывается при расчёте налога.";

            var isEmpty = taxPostingList.Postings.Count == 0;
            var type = taxPostingList.OperationType;

            if (isEmpty &&
                (type == OperationType.PaymentOrderIncomingMediationFee ||
                 type == OperationType.CashOrderIncomingMediationFee ||
                 type == OperationType.PaymentOrderIncomingMaterialAid ||
                 type == OperationType.CashOrderIncomingMaterialAid ||
                 type == OperationType.PaymentOrderIncomingLoanObtaining ||
                 type == OperationType.PaymentOrderOutgoingProfitWithdrawing ||
                 type == OperationType.PaymentOrderIncomingContributionOfOwnFunds ||
                 type == OperationType.PaymentOrderIncomingPaymentForGoods ||
                 type == OperationType.CashOrderOutgoingPaymentSuppliersForGoods ||
                 type == OperationType.PurseOperationTransferToSettlement))
            {
                return taxPostingList.Message ?? NonTaxableMessage;
            }

            return taxPostingList.Message ?? string.Empty;
        }
    }
}
