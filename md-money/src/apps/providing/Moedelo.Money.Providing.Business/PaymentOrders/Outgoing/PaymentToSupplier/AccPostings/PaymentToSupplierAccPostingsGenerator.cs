using Moedelo.AccPostings.Enums;
using Moedelo.LinkedDocuments.Enums;
using Moedelo.Money.Providing.Business.Abstractions.AccountingPostings.Models;
using Moedelo.Money.Providing.Business.AccountingPostings.Extensions;
using Moedelo.Money.Providing.Business.AccountingStatements.PaymentForDocuments.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using OperationType = Moedelo.AccPostings.Enums.OperationType;

namespace Moedelo.Money.Providing.Business.AccountingPostings.PaymentOrders.Outgoing
{
    static class PaymentToSupplierAccPostingsGenerator
    {
        private static readonly LinkedDocumentType[] AlowedDocumentTypes = new[]
        {
            LinkedDocumentType.Waybill,
            LinkedDocumentType.Statement,
            LinkedDocumentType.Upd,
            LinkedDocumentType.ReceiptStatement
        };

        public static IReadOnlyCollection<AccountingPosting> Generate(PaymentToSupplierAccPostingGenerateRequest request)
        {
            // сервис сохранит только одну проводку для денежной операции
            // проводка по признанию предоплаты оплатой генерируется в бухсправках
            var commonDocs = request.BaseDocuments
                .Where(x => AlowedDocumentTypes.Contains(x.Type))
                .ToArray();

            var accountingPosting = new AccountingPosting
            {
                Date = request.PaymentDate,
                Sum = request.PaymentSum,
                DebitCode = request.IsMainKontragent
                    ? SyntheticAccountCode._60_02
                    : SyntheticAccountCode._76_05,
                DebitSubcontos = new[]
                {
                    request.Kontragent.ToSubconto(),
                    request.Contract.ToSubconto()
                },
                CreditCode = SyntheticAccountCode._51_01,
                CreditSubcontos = new[]
                {
                    request.SettlementAccount.ToSubconto()
                },
                Description = AccPostingDescriptionHelper.GetPaymentPostingDescription(commonDocs, request.Contract),
                OperationType = OperationType.PaymentOrderOutgoingPaymentSuppliersForGoods
            };

            if (!request.IsStockInvisible)
                return new[] { accountingPosting };

            //если скрыт склад генерируем проводку закрывающего документа, а именно проводку Акта(Покумки)
            var incomingStatementPosting = new AccountingPosting
            {
                Date = request.PaymentDate,
                Sum = request.PaymentSum,
                DebitCode = SyntheticAccountCode._26_01,
                DebitSubcontos = new[]
                {
                    request.DivisionSubconto,
                    request.CostItemsSubconto
                },
                CreditCode = request.IsMainKontragent
                    ? SyntheticAccountCode._60_02
                    : SyntheticAccountCode._76_05,
                CreditSubcontos = new[]
                {
                    request.Kontragent.ToSubconto(),
                    request.Contract.ToSubconto()
                },
                Description = "Услуги производственного характера",
                OperationType = OperationType.PaymentOrderOutgoingPaymentSuppliersForGoods
            };

            return new[] { accountingPosting, incomingStatementPosting };

        }
    }
}
