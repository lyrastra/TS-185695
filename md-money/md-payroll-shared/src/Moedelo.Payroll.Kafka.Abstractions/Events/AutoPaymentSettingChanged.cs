using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Payroll.Enums.SalarySettings;

namespace Moedelo.Payroll.Kafka.Abstractions.Events
{ 
    public class AutoPaymentSettingChanged : IEntityEventData
    {
        /// <summary>
        /// Идентификатор фирмы
        /// </summary>
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
        /// Автоматическая выпалата аванса
        /// </summary>
        public bool? IsAutoAdvancePayment { get; set; }

        /// <summary>
        /// Автоматическая выпалата зарплаты
        /// </summary>
        public bool? IsAutoSalaryPayment { get; set; }

        /// <summary>
        /// Автоматическая выпалата ГПД
        /// </summary>
        public bool? IsAutoWorkContractPayment { get; set; }

        /// <summary>
        /// Период выплаты зарплаты: в расчетном месяце или следующим
        /// </summary>
        public SalaryPaymentPeriod SalaryPaymentPeriod { get; set; }
    }
}