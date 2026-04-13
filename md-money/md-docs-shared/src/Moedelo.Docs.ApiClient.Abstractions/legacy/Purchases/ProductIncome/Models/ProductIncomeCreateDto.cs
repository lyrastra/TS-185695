using System;
using System.Collections.Generic;

namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Purchases.ProductIncome.Models
{
    /// <summary>
    /// Приход без документов
    /// </summary>
    public class ProductIncomeCreateDto
    {
        /// <summary>
        /// Номер документа
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Дата документа
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Числовой индетификатор склада
        /// </summary>
        public long StockId { get; set; }

        /// <summary>
        /// Дополнительная информация
        /// </summary>
        public string AdditionalInfo { get; set; }

        /// <summary>
        /// Список позиций документа
        /// </summary>
        public List<ProductIncomeItemDto> Items { get; set; }
    }
}