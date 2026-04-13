using System;
using System.Collections.Generic;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto.Providing.TaxPostings;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier.Dto
{
    /// <summary>
    /// Модель для сохранения операции "Оплата поставщику"
    /// </summary>
    public class PaymentToSupplierSaveDto
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
        /// Идентификатор расчётного счёта
        /// </summary>
        public int SettlementAccountId { get; set; }

        /// <summary>
        /// Контрагент
        /// </summary>
        public ContractorSaveDto Contractor { get; set; }

        /// <summary>
        /// Договор
        /// </summary>
        public ContractSaveDto Contract { get; set; }

        /// <summary>
        /// Сумма платежа
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Назначение платежа
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// НДС
        /// </summary>
        public NdsDto Nds { get; set; }

        /// <summary>
        /// Признак: основной контрагент
        /// </summary>
        public bool? IsMainContractor { get; set; }

        /// <summary>
        /// Признак: нужно ли проводить в бухгалтерском учете
        /// </summary>
        public bool? ProvideInAccounting { get; set; }

        /// <summary>
        /// Связанные первичные документы
        /// </summary>
        public IReadOnlyCollection<DocumentLinkSaveDto> Documents { get; set; } = Array.Empty<DocumentLinkSaveDto>();

        /// <summary>
        /// Резерв (данная величина уменьшает сумму, покрываемую первичными документами)
        /// </summary>
        public decimal? ReserveSum { get; set; }

        /// <summary>
        /// Параметры налогового учёта
        /// </summary>
        public TaxPostingsSaveDto TaxPostings { get; set; }

        /// <summary>
        /// Оплачен
        /// </summary>
        public bool IsPaid { get; set; }

        /// <summary>
        /// Признак: нужно ли зафиксировать номер платежа в нумерации
        /// </summary>
        public bool IsSaveNumeration { get; set; }
    }
}
