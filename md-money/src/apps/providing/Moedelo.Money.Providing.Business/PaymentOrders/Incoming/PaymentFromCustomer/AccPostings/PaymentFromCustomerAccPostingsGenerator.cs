using Moedelo.AccPostings.Enums;
using Moedelo.LinkedDocuments.Enums;
using Moedelo.Money.Providing.Business.Abstractions.AccountingPostings.Models;
using Moedelo.Money.Providing.Business.AccountingPostings;
using Moedelo.Money.Providing.Business.PaymentOrders.Incoming.PaymentFromCustomer.Models;
using System.Linq;

namespace Moedelo.Money.Providing.Business.PaymentOrders.Incoming.PaymentFromCustomer.AccPostings
{
    internal static class PaymentFromCustomerAccPostingsGenerator
    {
        private static readonly LinkedDocumentType[] AlowedDocumentTypes = new[]
        {
            LinkedDocumentType.Waybill,
            LinkedDocumentType.Statement,
            LinkedDocumentType.SalesUpd
        };

        public static AccountingPosting Generate(PaymentFromCustomerAccPostingGenerateRequest request)
        {
            // сервис сохранит только одну проводку для денежной операции
            // проводка по признанию предоплаты оплатой генерируется в бухсправках
            var commonDocs = request.Documents
                .Where(x => AlowedDocumentTypes.Contains(x.Type))
                .ToArray();

            return new AccountingPosting
            {
                Date = request.Date,
                Sum = request.Sum,
                DebitCode = SyntheticAccountCode._51_01,
                DebitSubcontos = new[]
                {
                    new Subconto
                    {
                        Id = request.SettlementAccount.SubcontoId,
                        Name = request.SettlementAccount.Number,
                        Type = SubcontoType.SettlementAccount
                    }
                },
                CreditCode = MapAccountCode(request),
                CreditSubcontos = new[]
                {
                    new Subconto
                    {
                        Id = request.Kontragent.SubcontoId,
                        Name = request.Kontragent.Name,
                        Type = SubcontoType.Kontragent
                    },
                    new Subconto
                    {
                        Id = request.Contract.SubcontoId,
                        Name = request.Contract.Name,
                        Type = SubcontoType.Contract
                    }
                },
                Description = AccPostingDescriptionHelper.GetPaymentPostingDescription(commonDocs, request.Contract),
                OperationType = OperationType.PaymentOrderIncomingPaymentForGoods,
            };
        }

        private static SyntheticAccountCode MapAccountCode(PaymentFromCustomerAccPostingGenerateRequest businessModel)
        {
            if (businessModel.IsMediation || businessModel.IsMainKontragent)
            {
                return SyntheticAccountCode._62_02;
            }
            return SyntheticAccountCode._76_06;
        }
    }
}
