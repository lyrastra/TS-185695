using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.AspNetCore.Xss.Validation;
using Moedelo.Money.Api.Infrastructure.Validation;
using System;

namespace Moedelo.Money.Api.Models.PaymentOrders.Incoming.TransferFromAccount
{
    /// <summary>
    /// Модель для сохранения операции "Перевод со счета"
    /// </summary>
    public class TransferFromAccountSaveDto
    {
        /// <summary>
        /// Дата документа
        /// </summary>
        [DateValue]
        [RequiredValue]
        public DateTime Date { get; set; }

        /// <summary>
        /// Номер документа
        /// </summary>
        [RequiredValue]
        [PaymentOrderNumber]
        [ValidateXss]
        public string Number { get; set; }

        /// <summary>
        /// Идентификатор расчётного счёта с которого был осуществлен перевод
        /// </summary>
        [IdIntValue]
        [RequiredValue]
        public int FromSettlementAccountId { get; set; }

        /// <summary>
        /// Идентификатор расчётного счёта на который был осуществлен перевод
        /// </summary>
        [IdIntValue]
        [RequiredValue]
        public int SettlementAccountId { get; set; }

        /// <summary>
        /// Сумма платежа
        /// </summary>
        [SumValue]
        [RequiredValue]
        public decimal Sum { get; set; }

        /// <summary>
        /// Назначение платежа
        /// </summary>
        [ValidateXss]
        [PaymentOrderDescription]
        public string Description { get; set; }
    }
}