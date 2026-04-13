using System;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.TransferToAccount.Dto
{
    public class TransferToAccountSaveDto
    {
        /// <summary> Дата документа </summary>
        public DateTime Date { get; set; }

        /// <summary> Номер документа </summary>
        public string Number { get; set; }

        /// <summary> Идентификатор расчётного счёта с которого был осуществлен перевод </summary>
        public int SettlementAccountId { get; set; }

        /// <summary> Идентификатор расчётного счёта на который был осуществлен перевод </summary>
        public int ToSettlementAccountId { get; set; }

        /// <summary> Сумма платежа </summary>
        public decimal Sum { get; set; }

        /// <summary> Признак: нужно ли проводить в бухгалтерском учете </summary>
        public bool? ProvideInAccounting { get; set; }

        /// <summary> Оплачен </summary>
        public bool IsPaid { get; set; }
    }
}