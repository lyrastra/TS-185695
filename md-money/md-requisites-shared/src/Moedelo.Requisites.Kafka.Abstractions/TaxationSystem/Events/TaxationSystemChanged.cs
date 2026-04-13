using System;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Requisites.Enums.TaxationSystems;

namespace Moedelo.Requisites.Kafka.Abstractions.TaxationSystem.Events
{
    public class TaxationSystemChanged : IEntityEventData
    {
        public int FirmId { get; set; }
        public int Id { get; set; }

        /// <summary>Год начала периода</summary>
        public short StartYear { get; set; }

        /// <summary>Год окончания периода</summary>
        public short? EndYear { get; set; }

        /// <summary>УСН</summary>
        public bool IsUsn { get; set; }

        /// <summary>ЕНВД</summary>
        public bool IsEnvd { get; set; }

        /// <summary>ОСНО</summary>
        public bool IsOsno { get; set; }

        /// <summary>Вид УСН</summary>
        public UsnType UsnType { get; set; }

        /// <summary>Величина УСН доходы-расходы</summary>
        public double? UsnSize { get; set; }
    }
}