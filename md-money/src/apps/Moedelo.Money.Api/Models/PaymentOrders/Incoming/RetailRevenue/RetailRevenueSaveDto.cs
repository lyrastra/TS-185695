using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.AspNetCore.Xss.Validation;
using Moedelo.Money.Api.Infrastructure.Validation;
using Moedelo.Money.Api.Models.TaxPostings;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Api.Models.PaymentOrders.Incoming.RetailRevenue
{
    /// <summary>
    /// Модель для сохранения операции "Поступление за товар оплаченный банковской картой"
    /// </summary>
    public class RetailRevenueSaveDto : IValidatableObject
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
        /// Идентификатор расчётного счёта
        /// </summary>
        [IdIntValue]
        [RequiredValue]
        public int SettlementAccountId { get; set; }

        /// <summary>
        /// Идентификатор патента.
        /// </summary>
        [IdLongValue]
        [DefaultValue(null)]
        public long? PatentId { get; set; }

        /// <summary>
        /// Сумма платежа
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Назначение платежа
        /// </summary>
        [ValidateXss]
        [PaymentOrderDescription]
        public string Description { get; set; }
        
        /// <summary>
        /// НДС
        /// </summary>
        public NdsSaveDto Nds { get; set; }

        /// <summary>
        /// Эквайринг
        /// </summary>
        public AcquiringSaveDto Acquiring { get; set; }

        /// <summary>
        /// Налоговый учет.
        /// </summary>
        public TaxPostingsSaveDto TaxPostings { get; set; }

        /// <summary>
        /// Дата реализации/отгрузки
        /// </summary>
        [DateValue]
        public DateTime? SaleDate { get; set; }

        /// <summary>
        /// Учитывать в СНО
        /// УСН = 1
        /// ОСНО = 2
        /// ЕНВД = 3
        /// УСН + ЕНВД = 4
        /// ОСНО + ЕНВД = 5
        /// Патент = 6
        /// </summary>
        [EnumValue(EnumType = typeof(TaxationSystemType), AllowNull = true)]
        [DefaultValue(null)]
        public TaxationSystemType? TaxationSystemType { get; set; }

        /// <summary>
        /// Признак: нужно ли проводить в бухгалтерском учете
        /// </summary>
        [DefaultValue(true)]
        public bool? ProvideInAccounting { get; set; }

        /// <summary>
        /// Признак: Посреднечество
        /// </summary>
        public bool IsMediation { get; set; }

        private static readonly SumValueAttribute SumValueAttribute = new SumValueAttribute();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            if (Acquiring?.IsAcquiring == true && Acquiring?.CommissionSum > 0)
            {
                return results;
            }
            Validator.TryValidateValue(Sum, new ValidationContext(this, null, null) { MemberName = "Sum" }, results, new[] { SumValueAttribute });
            return results;
        }
    }
}