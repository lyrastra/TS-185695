using System;
using System.Collections.Generic;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.Statements.Dto
{
    public class SalesStatementSaveRequestDto
    {
        // <summary>
        /// Номер документа
        /// Если поле не заполнено, значение будет вычислено автоматически.
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Дата документа
        /// </summary>
        public DateTime? DocDate { get; set; }

        /// <summary>
        /// Id контрагента
        /// </summary>
        public int KontragentId { get; set; }

        /// <summary>
        /// Id связанного счета
        /// </summary>
        public long? BillId { get; set; }

        /// <summary>
        /// Id проекта
        /// </summary>
        public long? ProjectId { get; set; }

        /// <summary>
        /// Id посреднического договора
        /// </summary>
        public long? MiddlemanContractId { get; set; }

        /// <summary>
        /// Тип начисления НДС (счерху/в т.ч/не начислять)
        /// </summary>
        public NdsPositionType? NdsPositionType { get; set; }

        /// <summary>
        /// Счет-фактура
        /// </summary>
        public LinkedInvoiceDto Invoice { get; set; }

        /// <summary>
        /// Признак документа "Подписан" (Да, Нет, Скан)
        /// </summary>
        public string OnHands { get; set; }

        /// <summary>
        /// Дополнительная информация
        /// </summary>
        public string AdditionalInfo { get; set; }

        /// <summary>
        /// Статус "Печать и подпись"
        /// </summary>
        public bool UseStampAndSign { get; set; }

        /// <summary>
        /// Список позиций
        /// </summary>
        public List<SalesStatementItemSaveRequestDto> Items { get; set; }

        /// <summary>
        /// Система налогообложения, в которой будет учтён документ (1 - УСНО, 2 - ОСНО, 3 - ЕНВД)
        /// </summary>
        public TaxationSystemType? TaxationSystemType { get; set; }
    }
}