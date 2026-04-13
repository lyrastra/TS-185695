using Moedelo.AccountingV2.Dto.PrimaryDocuments;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.Documents;
using System;
using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.Statements
{
    public class StatementWithItemDto
    {

        /// <summary>
        /// Id документа
        /// </summary>
        public long Id { get; set; }

        public string Number { get; set; }

        /// <summary>
        /// BaseId документа
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Дата документа
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Контрагент
        /// </summary>
        public int KontragentId { get; set; }

        /// <summary>
        /// Счета
        /// </summary>
        public List<long?> BillIds { get; set; }

        /// <summary>
        /// По договору
        /// </summary>
        public long? ProjectId { get; set; }

        /// <summary>
        /// По договору в т.ч. основному
        /// </summary>
        public long? ContractId { get; set; }

        /// <summary>
        /// Счет контрагента
        /// </summary>
        public int KontragentAccountCode { get; set; }

        /// <summary>
        /// Тип начисления НДС 0 - Нет, 1 - Сверху, 2 - В том числе
        /// </summary>
        public NdsPositionType NdsPositionType { get; set; }

        /// <summary>
        /// Признак документа "Подписан" (Да, Нет, Скан)
        /// </summary>
        public string OnHands { get; set; }

        /// <summary>
        /// Сумма документа
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Учесть в (при сдвоенной СНО)
        /// </summary>
        public TaxationSystemType? TaxationSystemType { get; set; }

        /// <summary>
        /// Связанный счет-фактура
        /// </summary>
        public LinkedInvoiceDto Invoice { get; set; }

        /// <summary>
        /// Позиции документа
        /// </summary>
        public List<ItemDto> Items { get; set; }

        public DocumentStatus Status { get; set; }

        /// <summary>
        /// Проводить акт в бух.учете
        /// </summary>
        public bool ProvideInAccounting { get; set; }

        /// <summary>
        /// Исходящий акт по услугам, связанными с основными видами деятельности
        /// </summary>
        public bool IsMainActivity { get; set; }

        /// <summary>
        /// Id посреднического договора
        /// </summary>
        public long? MiddlemanContractId { get; set; }

        /// <summary>
        /// Дополнительная информация
        /// </summary>
        public string AdditionalInfo { get; set; }

        /// <summary>
        /// Использовать подпись и печать
        /// </summary>
        public bool UseStampAndSign { get; set; }
    }
}
