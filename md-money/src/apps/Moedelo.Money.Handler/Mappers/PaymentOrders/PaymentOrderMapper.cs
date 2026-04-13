using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.Commands.ActualizeFromImport;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.Commands.ChangeIsPaidFromIntegration;
using System.Linq;

namespace Moedelo.Money.Handler.Mappers.PaymentOrders
{
    internal static class PaymentOrderMapper
    {
        public static ActualizeRequestItem[] Map(ActualizeFromImport commandData)
        {
            return commandData.Items.Select(Map).ToArray();
        }

        private static ActualizeRequestItem Map(ActualizeFromImportItem commandDataItem)
        {
            return new ActualizeRequestItem
            {
                DocumentBaseId = commandDataItem.DocumentBaseId,
                Date = commandDataItem.Date,
                IsOutsourceApproved = commandDataItem.IsOutsourceApproved
            };
        }
        
        public static ChangeIsPaidRequestItem Map(ChangeIsPaidFromIntegrationItem commandDataItem)
        {
            return new ChangeIsPaidRequestItem
            {
                DocumentBaseId = commandDataItem.DocumentBaseId,
                Date = commandDataItem.Date,
                IsPaid = commandDataItem.IsPaid,
                PaymentNumber = commandDataItem.PaymentNumber,
                PayerSettlementNumber = commandDataItem.PayerSettlementNumber
            };
        }
    }
}