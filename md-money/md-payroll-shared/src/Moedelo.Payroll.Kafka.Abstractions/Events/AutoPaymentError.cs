using System;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Payroll.Enums.PaymentEvents;

namespace Moedelo.Payroll.Kafka.Abstractions.Events
{
    public class AutoPaymentError : IEntityEventData
    {
        public PaymentEventType EventType { get; set; }
        
        public DateTime ChargeDate  { get; set; }
        
        public bool HasIntegration { get; set; }
        
        public string EventDescription { get; set; } 
        
        public int FirmId { get; set; }
        
        public bool IsValidation { get; set; }
        
        public string ErrorDescription { get; set; }

        public bool IsCompleteEvent { get; set; }

        public int? CalendarId { get; set; }
    }
}