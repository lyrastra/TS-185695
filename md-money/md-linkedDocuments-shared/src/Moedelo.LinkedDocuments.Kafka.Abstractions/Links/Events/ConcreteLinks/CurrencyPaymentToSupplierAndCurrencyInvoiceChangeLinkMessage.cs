using Moedelo.Common.Kafka.Abstractions.Base;

namespace Moedelo.LinkedDocuments.Kafka.Abstractions.Links.Events.ConcreteLinks
{
    /// <summary>
    /// Событие по изменению связей валютный плетеж поставщику - инвойс (покупки) 
    /// </summary>
    public class CurrencyPaymentToSupplierAndCurrencyInvoiceChangeLinkMessage : MoedeloKafkaMessageValueBase
    {
        /// <summary>
        /// Созданные связи
        /// </summary>
        public CurrencyPaymentToSupplierAndCurrencyInvoiceLink[] CreatedLinks { get; set; }
        
        /// <summary>
        /// Удаленные связи
        /// </summary>
        public CurrencyPaymentToSupplierAndCurrencyInvoiceLink[] DeletedLinks { get; set; }
    }
}