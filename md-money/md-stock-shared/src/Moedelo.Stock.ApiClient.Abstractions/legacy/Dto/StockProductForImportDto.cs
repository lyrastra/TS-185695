using Moedelo.Docs.Enums;
using Moedelo.Stock.Enums;
using NdsPositionType = Moedelo.Stock.Enums.NdsPositionType;

namespace Moedelo.Stock.ApiClient.Abstractions.legacy.Dto
{
    public class StockProductForImportDto
    {
        /// <summary>
        /// Идентификатор товара
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// Идентификатор номенклатуры
        /// </summary>
        public long NomenclatureId { get; set; }
        
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Артикул
        /// </summary>
        public string Article { get; set; }
     
        /// <summary>
        /// НДС
        /// </summary>
        public NdsTypes Nds { get; set; }

        /// <summary>
        /// Тип НДС
        /// </summary>
        public NdsPositionType NdsPositionType { get; set; }
        
        /// <summary>
        /// Единица измерения
        /// </summary>
        public string UnitOfMeasurement { get; set; }
        
        public StockProductTypeEnum Type { get; set; }
        
        /// <summary>
        /// Является ли номанклатура комплектом
        /// </summary>
        public bool IsBundle { get; set; }
        
        public StockProductSubTypeEnum ProductSubType { get; set; }
        
        /// <summary>
        /// Использовать ставку НДС из Учетной политики
        /// </summary>
        public bool UseNdsFromAccPolicy { get; set; }
    }
}