using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.Json.Convertors;
using Moedelo.Money.Enums;
using Moedelo.Money.Enums.Extensions;
using Newtonsoft.Json;
using System;
using Moedelo.Money.Api.Models.PaymentOrders.Outgoing.BudgetaryPayment.CurrencyInvoiceNdsPayments;

namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.BudgetaryPayment
{
    public class BudgetaryPaymentResponseDto
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
        /// Сумма платежа (7)
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Назначение платежа (24)
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Тип бюджетного платежа
        /// 0 — налог
        /// 1 — взнос
        /// </summary>
        public BudgetaryAccountType AccountType => AccountCode.IsSocialInsurance()
            ? BudgetaryAccountType.Fees
            : BudgetaryAccountType.Taxes;

        /// <summary>
        /// Бух.счет для типа налога/взноса
        /// </summary>
        public BudgetaryAccountCodes AccountCode { get; set; }

        /// <summary>
        /// Тип платежа
        /// </summary>
        public KbkPaymentType KbkPaymentType { get; set; }

        /// <summary>
        /// КБК
        /// </summary>
        public BudgetaryKbkResponseDto Kbk { get; set; }

        /// <summary>
        /// Бюджетный период
        /// </summary>
        public BudgetaryPeriodResponseDto Period { get; set; }

        /// <summary>
        /// Статус плательщика (101)
        /// </summary>
        public BudgetaryPayerStatus PayerStatus { get; set; }

        /// <summary>
        /// Основание платежа (106)
        /// </summary>
        public BudgetaryPaymentBase PaymentBase { get; set; }

        /// <summary>
        /// Дата требования/исполнительного документа (109)
        /// </summary>
        public string DocumentDate { get; set; }

        /// <summary>
        /// Номер требования/исполнительного документа (108)
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// УИН (22)
        /// </summary>
        public string Uin { get; set; }

        /// <summary>
        /// Реквизиты получателя
        /// </summary>
        public BudgetaryRecipientResponseDto Recipient { get; set; }

        /// <summary>
        /// Признак: нужно ли проводить в бухгалтерском учете
        /// </summary>
        public bool ProvideInAccounting { get; set; }

        /// <summary>
        /// Признак: проведено вручную в налоговом учете
        /// </summary>
        public bool TaxPostingsInManualMode { get; set; }

        /// <summary>
        /// Признак: Оплачен
        /// </summary>
        public bool IsPaid { get; set; }

        public bool IsReadOnly { get; set; }

        public int? TradingObjectId { get; set; }

        public OperationType OperationType => OperationType.BudgetaryPayment;
        
        public TaxationSystemType? TaxationSystemType { get; set; }
        
        public long? PatentId { get; set; }
        
        /// <summary>
        /// Связанные инвойсы на покупку (оплата НДС импортируемых товаров и услуг)
        /// </summary>
        public RemoteServiceResponseDto<CurrencyInvoiceResponseDto[]> CurrencyInvoices { get; set; }

        /// <summary>
        /// Операция создана из импорта
        /// </summary>
        public bool IsFromImport { get; set; }
    }
}
