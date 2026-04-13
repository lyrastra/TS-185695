using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Payroll.Shared.Enums.SalarySettings;

namespace Moedelo.Payroll.Kafka.Abstractions.Events
{ 
    public class FirmChargeSettingChanged : IEntityEventData
    {
        /// <summary>
        /// Идентификатор фирмы
        /// </summary>
        public int FirmId { get; set; }

        /// <summary>
        /// Тип настройки начислений
        /// </summary>
        public FirmChargeSettingType FirmChargeSettingType { get; set; }
    }
}