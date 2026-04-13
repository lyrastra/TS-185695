using System;
using System.ComponentModel;
using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.Json.Convertors;
using Moedelo.Money.Api.Models.LinkedDocuments;
using Moedelo.Money.Enums;
using Newtonsoft.Json;

namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.Deduction
{
    /// <summary>
    /// Операция "Выплаты удержаний"
    /// </summary>
    public class DeductionResponseDto
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
        /// Идентификатор расчётного счёта
        /// </summary>
        public int SettlementAccountId { get; set; }

        /// <summary>
        /// Контрагент
        /// </summary>
        public ContractorResponseDto Contractor { get; set; }

        /// <summary>
        /// Договор
        /// </summary>
        public RemoteServiceResponseDto<ContractDto> Contract { get; set; }

        /// <summary>
        /// Сумма платежа
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Назначение платежа
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Очередность платежа
        /// </summary>
        public PaymentPriority PaymentPriority { get; set; }

        /// <summary>
        /// ОКТМО
        /// </summary>
        public string Oktmo { get; set; }

        /// <summary>
        /// Код бюджетной классификации (КБК) (104)
        /// </summary>
        public string Kbk { get; set; }

        /// <summary>
        /// Номер документа (108)
        /// </summary>
        public string DeductionWorkerDocumentNumber { get; set; }

        /// <summary>
        /// УИН (22)
        /// </summary>
        public string Uin { get; set; }

        /// <summary>
        /// Статус плательщика (101)
        /// </summary>
        public BudgetaryPayerStatus PayerStatus { get; set; }

        /// <summary>
        /// Оплачен
        /// </summary>
        public bool IsPaid { get; set; }

        /// <summary>
        /// Только "для чтения"
        /// </summary>
        public bool IsReadOnly { get; set; }

        /// <summary>
        /// Тип платежа
        /// </summary>
        public OperationType OperationType => OperationType.PaymentOrderOutgoingDeduction;

        /// <summary>
        /// Операция создана из импорта
        /// </summary>
        public bool IsFromImport { get; set; }

        /// <summary>
        /// Взыскивается долг перед бюджетом
        /// </summary>
        public bool IsBudgetaryDebt => DeductionWorkerId.HasValue;

        /// <summary>
        /// Сотрудник, у которого удерживается сумма
        /// </summary>
        public int? DeductionWorkerId { get; set; }

        /// <summary>
        /// ИНН сотрудника, у которого удерживается сумма
        /// </summary>
        public string DeductionWorkerInn { get; set; }

        /// <summary>
        /// ФИО сотрудника, у которого удерживается сумма
        /// </summary>
        public string DeductionWorkerFio { get; set; }

        /// <summary>
        /// Признак: проведено в бухгалтерском учете
        /// </summary>
        public bool ProvideInAccounting { get; set; }
    }
}