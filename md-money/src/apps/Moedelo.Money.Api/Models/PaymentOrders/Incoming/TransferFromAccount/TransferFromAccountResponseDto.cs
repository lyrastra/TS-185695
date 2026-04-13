using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.Json.Convertors;
using Moedelo.Money.Enums;
using Newtonsoft.Json;
using System;

namespace Moedelo.Money.Api.Models.PaymentOrders.Incoming.TransferFromAccount
{
    /// <summary>
    /// Операция "Перевод со счета"
    /// </summary>
    public class TransferFromAccountResponseDto
    {
        /// <summary>
        /// Дата документа
        /// </summary>
        [DateValue]
        [JsonConverter(typeof(IsoDateConverter))]
        public DateTime Date { get; set; }

        /// <summary>
        /// Номер документа
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Идентификатор расчётного счёта с которого был осуществлен перевод
        /// </summary>
        public int FromSettlementAccountId { get; set; }

        /// <summary>
        /// Идентификатор расчётного счёта на который был осуществлен перевод
        /// </summary>
        public int SettlementAccountId { get; set; }

        /// <summary>
        /// Сумма платежа
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Назначение платежа
        /// </summary>
        public string Description { get; set; }

        public OperationType OperationType => OperationType.PaymentOrderIncomingTransferFromAccount;

        /// <summary>
        /// Только "для чтения"
        /// </summary>
        public bool IsReadOnly { get; set; }

        /// <summary>
        /// Операция создана из импорта
        /// </summary>
        public bool IsFromImport { get; set; }
        
        public OperationState OperationState { get; set; }
    }
}