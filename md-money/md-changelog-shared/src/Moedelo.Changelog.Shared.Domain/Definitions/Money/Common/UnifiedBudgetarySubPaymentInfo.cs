using System.ComponentModel.DataAnnotations;

namespace Moedelo.Changelog.Shared.Domain.Definitions.Money.Common
{
    public class UnifiedBudgetarySubPaymentInfo
    {
        public long DocumentBaseId { get; set; }

        [Display(Name = "Сумма")]
        public MoneySum Sum { get; set; }

        [Display(Name = "Тип бюджетного платежа")]
        public BudgetaryAccountType AccountType { get; set; }

        [Display(Name = "Тип налога/взноса")]
        public string AccountCode { get; set; }

        [Display(Name = "Период")]
        public string Period { get; set; }

        // на всякий случай сохраняем технический идентификатор
        public int KbkId { get; set; }

        [Display(Name = "КБК")]
        public string KbkNumber { get; set; }

        [Display(Name = "Тип платежа (КБК)")]
        public KbkPaymentTypeEnum KbkPaymentType { get; set; }

        [Display(Name = "Патент")]
        public string PatentName { get; set; }

        // сохраняется на всякий случай, можно и не сохранять
        public long? PatentId { get; set; }

        public int? TradingObjectId { get; set; }

        [Display(Name = "Проведено вручную в налоговом учете")]
        public bool IsManualTaxPostings { get; set; }

        /// <summary>
        /// Тип платежа (КБК)
        /// </summary>
        public enum KbkPaymentTypeEnum
        {
            /// <summary>
            /// Налоги
            /// </summary>
            [Display(Name = "Налоги")]
            PaymentTaxes = 100,

            /// <summary>
            /// Взносы
            /// </summary>
            [Display(Name = "Взносы")]
            PaymentFees = 101,

            /// <summary>
            /// Пени
            /// </summary>
            [Display(Name = "Пени")]
            Surcharge = 2,

            /// <summary>
            /// Штрафы
            /// </summary>
            [Display(Name = "Штрафы")]
            Forfeit = 3
        }

        public enum BudgetaryAccountType
        {
            [Display(Name = "Налоги")]
            Taxes = 0,
            [Display(Name = "Взносы")]
            Fees = 1
        }
    }
}
