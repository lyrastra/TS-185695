using System;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Infrastructure;
using Moedelo.Money.Enums;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Incoming
{
    [OperationType(OperationType.PaymentOrderIncomingRefundFromAccountablePerson)]
    public class ConfirmRefundFromAccountablePersonDto : IConfirmPaymentOrderDto
    {
        public long DocumentBaseId { get; set; }
        public DateTime Date { get; set; }
        public string Number { get; set; }
        public decimal Sum { get; set; }
        public string Description { get; set; }
        public int SettlementAccountId { get; set; }
        /// <summary>
        /// Плательщик (сотрудник)
        /// </summary>
        public ConfirmContractorDto Contractor { get; set; }
    }
}
