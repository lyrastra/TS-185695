using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Stocks;

namespace Moedelo.StockV2.Dto.Operations
{
    public class StockOperationCreateDto
    {
        /// <summary>
        /// DocumentBaseId документа, по которому создана операция
        /// </summary>
        public long SourceDocumentId { get; set; }

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
        /// Id контрагента (кажется, не используется, но заполняется в накладных)
        /// </summary>
        public int? ClientId { get; set; }

        /// <summary>
        /// Список операций над продуктами
        /// </summary>
        public List<OverProductDto> OperationsOverProducts { get; set; }

        public class OverProductDto
        {
            /// <summary>
            /// Продукт
            /// </summary>
            public long ProductId { get; set; }
            
            /// <summary>
            /// Склад, на котором производится операция 
            /// </summary>
            public long StockId { get; set; }

            /// <summary>
            /// Количество продукта
            /// </summary>
            public decimal Count { get; set; }

            /// <summary>
            /// Сумма операции
            /// </summary>
            public decimal Sum { get; set; }

            /// <summary>
            /// Флаг "За баланс" для требования-накладной
            /// </summary>
            public bool? IsOffbalance { get; set; }

            /// <summary>
            /// Код типа операции над продуктом
            /// </summary>
            public StockOperationTypeEnum TypeCode { get; set; }
        }
        
        public class ResultDto
        {
            public long Id { get; set; }
        }
    }
}