using System;
using System.Collections.Generic;
using Moedelo.Stock.Enums;

namespace Moedelo.Stock.ApiClient.Abstractions.legacy.Dto.StockOperations
{
    public class StockOperationDto
    {
        public long Id { get; set; }

        /// <summary>
        /// DocumentBaseId документа, по которому создана операция
        /// </summary>
        public long? SourceDocumentId { get; set; }

        /// <summary>
        /// Номер операции
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Дата операции
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Проведена ли операция в БУ
        /// </summary>
        public bool ProvideInAccounting { get; set; }

        /// <summary>
        /// Код типа операции
        /// </summary>
        public StockOperationTypeEnum TypeCode { get; set; }

        /// <summary>
        /// Код группы, в которую входит тип операции
        /// </summary>
        public StockOperationParentTypeEnum TypeParentCode { get; set; }

        /// <summary>
        /// Список операций над продуктами
        /// </summary>
        public List<StockOperationOverProductDto> OperationsOverProducts { get; set; }

        /// <summary>
        /// Id контрагента
        /// </summary>
        public int? ClientId { get; set; }
    }
}