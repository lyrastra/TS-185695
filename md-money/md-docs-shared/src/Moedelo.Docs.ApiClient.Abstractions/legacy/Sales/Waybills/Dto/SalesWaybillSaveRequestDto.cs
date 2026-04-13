using System;
using System.Collections.Generic;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.Waybills.Dto
{
    public class SalesWaybillSaveRequestDto
    {
        /// <summary>
        /// Номер документа
        /// Если поле не заполнено, значение будет вычислено автоматически.
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Дата документа (обязательное поле, nullable - по технических причинам)
        /// </summary>
        public DateTime? DocDate { get; set; }

        /// <summary>
        /// Тип начисления НДС 1 - не начислять, 2 - сверху, 3 - в том числе
        /// </summary>
        public NdsPositionType? NdsPositionType { get; set; }

        /// <summary>
        /// Id грузоотправителя (если указан)
        /// </summary>
        public int? SenderId { get; set; }

        /// <summary>
        /// Id поставщика (если указан)
        /// </summary>
        public int? SupplierId { get; set; }

        /// <summary>
        /// Id грузополучателя (если указан)
        /// </summary>
        public int? ReceiverId { get; set; }

        /// <summary>
        /// Id плательщика (если указан)
        /// </summary>
        public int? PayerId { get; set; }

        /// <summary>
        /// Id контрагента (обязательное поле)
        /// </summary>
        public int KontragentId { get; set; }

        /// <summary>
        /// Id связанного счета (если есть)
        /// </summary>
        public long? BillId { get; set; }

        /// <summary>
        /// Id связанного договора (если есть)
        /// </summary>
        public long? ProjectId { get; set; }

        /// <summary>
        /// Счет-фактура
        /// Ограничение: нельзя создавать связанный счет-фактуру при выключенном НДС (в этом случае д.б. равно null)
        /// </summary>
        public LinkedInvoiceDto Invoice { get; set; }

        /// <summary>
        /// Признак документа "Подписан" (Да, Нет, Скан)
        /// </summary>
        public string OnHands { get; set; }

        /// <summary>
        /// Id склада, с которого происходит продажа (если указан)
        /// </summary>
        public long? StockId { get; set; }

        /// <summary>
        /// Позиции документа (список не может быть пустым)
        /// </summary>
        public List<SalesWaybillSaveItemRequestDto> Items { get; set; }
        
        /// <summary>
        /// Учитывается в системе налогообложения
        /// 1 - УСН, 2 - ОСНО, 3 - ЕНВД
        /// </summary>
        public TaxationSystemType? TaxationSystemType { get; set; }
        
        /// <summary>
        /// Статус "Печать и подпись"
        /// </summary>
        public bool UseStampAndSign { get; set; }
    }
}