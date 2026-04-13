using System;
using System.Collections.Generic;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.CurrencyPaymentToSupplier.Models
{
    public class CurrencyPaymentToSupplierDto
    {
        /// <summary>
        /// Дата документа
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Номер документа
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Контрагент
        /// </summary>
        public ContractorDto Contractor { get; set; }

        /// <summary>
        /// Итоговая сумма платежа в рублях
        /// </summary>
        public decimal TotalSum { get; set; }

        /// <summary>
        /// Сумма платежа в валюте
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Признак: проведено вручную в налоговом учете
        /// </summary>
        public bool TaxPostingsInManualMode { get; set; }

        /// <summary>
        /// Признак оплачено пп
        /// </summary>
        public bool IsPaid { get; set; }

        /// <summary>
        /// Связанные первичные документы
        /// </summary>
        public RemoteServiceResponseDto<IReadOnlyCollection<DocumentLinkDto>> Documents { get; set; }

        /// <summary>
        /// Договор
        /// </summary>
        public RemoteServiceResponseDto<ContractDto> Contract { get; set; }
    }
}