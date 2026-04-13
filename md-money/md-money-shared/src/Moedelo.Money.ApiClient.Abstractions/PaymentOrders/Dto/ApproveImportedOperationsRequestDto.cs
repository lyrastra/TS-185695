using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto
{
    public class ApproveImportedOperationsRequestDto
    {
        public MoneySourceType? SourceType { get; set; }
        public long? SourceId { get; set; }
        public DateTime? Skipline { get; set; }
    }
}
