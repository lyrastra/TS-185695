using System;
using System.Collections.Generic;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Waybills.Dto
{
    public class WaybillDto
    {
        public int Id { get; set; }
        
        public long DocumentBaseId { get; set; }
        
        public string Number { get; set; }
        
        public DateTime Date { get; set; }
        
        public long? SubcontoId { get; set; }
        
        public int? ProjectId { get; set; }
        
        public WaybillTypeCode WaybillTypeCode { get; set; }
        
        public List<WaybillItemDto> Items { get; set; }
        
        public int KontragentId { get; set; }

        /// <summary>
        /// Счет контрагента (в Учетке)
        /// </summary>
        public int KontragentAccountCode { get; set; }

        /// <summary>
        /// Учесть в(выбранная СНО, Учетка)
        /// </summary>
        public TaxationSystemType? TaxationSystemType { get; set; }

        public PrimaryDocTransferDirection Direction { get; set; }

        /// <summary>
        /// Признак: Накладная для создания основного средства (только для покупок)
        /// </summary>
        public bool IsFromFixedAssetInvestment { get; set; }

        /// <summary>
        /// Номер забытого документа
        /// </summary>
        public string ForgottenDocumentNumber { get; set; }

        /// <summary>
        /// Дата забытого документа
        /// </summary>
        public DateTime? ForgottenDocumentDate { get; set; }
        
        public long? StockId { get; set; }

        /// <summary>
        /// Тип учета НУ: автоматически/вручную
        /// </summary>
        public ProvidePostingType TaxPostingType { get; set; }
        
        /// <summary>
        /// Тип начисления НДС (сверху/в т.ч.)
        /// </summary>
        public NdsPositionType NdsPositionType { get; set; }
    }
}