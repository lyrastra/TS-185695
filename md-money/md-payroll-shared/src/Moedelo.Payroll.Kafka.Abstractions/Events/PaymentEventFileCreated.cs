using System;
using System.Collections.Generic;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Payroll.Enums.PaymentEvents;
using Moedelo.Payroll.Enums.PaymentMethods;

namespace Moedelo.Payroll.Kafka.Abstractions.Events
{
    public class PaymentEventFileCreated : IEntityEventData
    {
        public PaymentEventType EventType { get; set; }
        
        public DateTime ChargeDate  { get; set; }

        public int CalendarId { get; set; }
        
        public bool HasIntegration { get; set; }
        
        public string Description { get; set; } 
        
        public int? FileId { get; set; } 
        
        public int FirmId { get; set; }
        
        public bool IsAutoEvent { get; set; }
        
        /// <summary>
        /// Есть ЗП Проект
        /// </summary>
        public bool IsHaveSalaryProject { get; set; }

        /// <summary>
        /// ЗП проект с резервированием?
        /// </summary>
        public bool IsSalaryProjectWithReserve { get; set; }

        /// <summary>
        /// Есть платежное поручение (ПП)
        /// </summary>
        public bool IsHavePaymentOrder { get; set; }

        /// <summary>
        /// Есть ПП по ЗП проекту
        /// </summary>
        public bool IsHaveSalaryProjectPaymentOrder { get; set; }
            
        /// <summary>
        /// Дата выплаты
        /// </summary>
        public DateTime PayDate  { get; set; }

        /// <summary>
        /// Реестр по ЗП проекту отправлен через интеграцию
        /// </summary>
        public bool IsExportSalaryProjectRegistry { get; set; }
        
        /// <summary>
        /// Список платежных методов
        /// </summary>
        public List<PaymentMethodType> PaymentMethods { get; set; }

        /// <summary>
        /// Общая сумма выплаты сотрудникам
        /// </summary>
        public decimal PaymentSum { get; set; }

        /// <summary>
        /// Список НДФЛ платежей
        /// </summary>
        public List<PaymentEventNdfl> PaymentNdfls { get; set; }
        
        /// <summary>
        /// Список дат начислений
        /// </summary>
        public List<DateTime> PaymentChargeDates { get; set; }
        
        /// <summary>
        /// Наличие платёжки по СФР травматизм
        /// </summary>
        public bool IsSfrInjuredPayment { get; set; }
        
        /// <summary>
        /// Список сотрудников с выплаченными удержаниями
        /// </summary>
        public List<string> WorkersWithDeductions { get; set; }
    }
}