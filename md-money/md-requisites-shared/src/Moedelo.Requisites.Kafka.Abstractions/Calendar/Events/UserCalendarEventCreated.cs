using System;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Requisites.Enums.Calendar;

namespace Moedelo.Requisites.Kafka.Abstractions.Calendar.Events
{
    public class UserCalendarEventCreated : IEntityEventData
    {
        /// <summary> Id </summary>
        public int Id { get; set; }

        /// <summary> Статус ивента </summary>
        public CalendarEventStatus Status { get; set; }
        
        /// <summary> Id фирмы </summary>
        public int FirmId { get; set; }

        /// <summary> Заголовок </summary>
        public string Title { get; set; }

        /// <summary> Дата начала </summary>
        public DateTime StartDate { get; set; }

        /// <summary> Дата окончания </summary>
        public DateTime EndDate { get; set; }

        /// <summary> Тип события </summary>
        public CalendarEventProtoId ProtoId { get; set; }

        public int? Year { get; set; }

        public int? Period { get; set; }
    }
}
