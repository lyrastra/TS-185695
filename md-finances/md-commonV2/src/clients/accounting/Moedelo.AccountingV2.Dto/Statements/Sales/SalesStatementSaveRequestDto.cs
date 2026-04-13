using System;
using System.Collections.Generic;
using Moedelo.AccountingV2.Dto.PrimaryDocuments;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.AccountingV2.Dto.Statements.Sales
{
    public class SalesStatementSaveRequestDto
    {
        /// <summary>
        /// Id документа
        /// </summary>
        public long Id { get; set; }

        /// <summary>
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
        /// Id связанного счета. Взаимоисключающий с <see cref="BillIds"/>.
        /// </summary>
        public long? BillId { get; set; }

        /// <summary>
        /// Ids связанных счетов. Взаимоисключающий с <see cref="BillId"/>.
        /// </summary>
        public long[] BillIds { get; set; }

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
        /// Список позиций
        /// </summary>
        public List<SalesStatementItemRequestDto> Items { get; set; }

        //public bool UseNds => NdsPositionType.UseNds();

        /// <summary>
        /// Статус "Печать и подпись"
        /// </summary>
        public bool UseStampAndSign { get; set; }

        /// <summary>
        /// Система налогообложения, в которой будет учтён документ (1 - УСНО, 2 - ОСНО, 3 - ЕНВД)
        /// </summary>
        public TaxationSystemType? TaxationSystemType { get; set; }
    }
}