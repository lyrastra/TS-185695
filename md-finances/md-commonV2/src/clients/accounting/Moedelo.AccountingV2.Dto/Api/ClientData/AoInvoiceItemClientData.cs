using System;
using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.AccountingV2.Dto.Api.ClientData
{
    public class AoInvoiceItemClientData : INdsDocumentClientData
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        public string Unit { get; set; }

        public string UnitCode { get; set; }

        public decimal Count { get; set; }
        
        /// <summary> Цена без НДС </summary>
        public decimal Price { get; set; }

        public NdsType NdsType { get; set; }

        /// <summary> Сумма без НДС </summary>
        public decimal SumWithoutNds { get; set; }

        /// <summary> Сумма НДС </summary>
        public decimal NdsSum { get; set; }

        /// <summary> Сумма с НДС </summary>
        public decimal SumWithNds { get; set; }

        /// <summary> Указал ли пользователь суммы сам </summary>
        public bool IsCustomSums { get; set; }

        /// <summary> НДС к вычету </summary>
        public decimal? NdsDeductionSum { get; set; }

        /// <summary>
        /// Код
        /// это Moedelo.Infrastructure.Domain.Nds.NdsOperationTypes
        /// </summary>
        public int? NdsOperationType { get; set; }
    }
}