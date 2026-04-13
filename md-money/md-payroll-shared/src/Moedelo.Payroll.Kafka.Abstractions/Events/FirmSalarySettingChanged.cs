using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Payroll.Enums.SalarySettings;

namespace Moedelo.Payroll.Kafka.Abstractions.Events
{ 
    public class FirmSalarySettingChanged : IEntityEventData
    {
        public int Id { get; set; }

        public int FirmId { get; set; }

        /// <summary>
        /// День выплаты зарплаты
        /// </summary>
        public int DaySalaryPayment { get; set; }

        /// <summary>
        /// день выплаты аванса 
        /// </summary>
        public int DayAdvancePayment { get; set; }

        /// <summary>
        /// Код территориальных условий (если есть РК) 
        /// </summary>
        public TerritorialConditionType TerritorialCondition { get; set; }

        /// <summary>
        /// Расчетный счет с которого платится зарплата
        /// </summary>
        public int? SalarySettlementAccountId { get; set; }

        /// <summary>
        /// Период выплаты зарплаты: в расчетном месяце или следующим
        /// </summary>
        public SalaryPaymentPeriod SalaryPaymentPeriod { get; set; }
    }
}