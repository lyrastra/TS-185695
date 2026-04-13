using Moedelo.Common.Enums.Enums.Bills;
using Moedelo.Common.Enums.Enums.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moedelo.AccountingV2.Dto.PrimaryDocuments
{
    public class PreviewItemDto
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Количество (Нетто)
        /// </summary>
        public decimal Count { get; set; }

        /// <summary>
        /// Единицы измерения
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// Код единицы измерения по ОКЕИ
        /// </summary>
        public string OKEI { get; set; }

        /// <summary>
        /// Тип позиции (товар/материал)
        /// </summary>
        public ItemType Type { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Тип НДС
        /// </summary>
        public NdsType NdsType { get; set; }

        /// <summary>
        /// Стоимость без ндс
        /// </summary>
        public decimal SumWithoutNds { get; set; }

        /// <summary>
        /// Размер НДС
        /// </summary>
        public decimal NdsSum { get; set; }

        /// <summary>
        /// Стоимость с ндс
        /// </summary>
        public decimal SumWithNds { get; set; }
    }
}
