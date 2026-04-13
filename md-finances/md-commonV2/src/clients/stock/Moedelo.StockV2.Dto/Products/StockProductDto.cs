using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.Documents;
using Moedelo.Common.Enums.Enums.Stocks;
using Moedelo.Common.Enums.Enums.SyntheticAccounts;

namespace Moedelo.StockV2.Dto.Products
{
    public class StockProductDto
    {
        public long Id { get; set; }

        /// <summary>
        /// Id номенклатурной группы
        /// </summary>
        public long NomenclatureId { get; set; }

        /// <summary>
        /// Id cчета, к которому привязан материал
        /// </summary>
        public long? SyntheticAccountId { get; set; }

        /// <summary>
        /// Код cчета, к которому привязан материал
        /// </summary>
        public SyntheticAccountCode SyntheticAccountCode { get; set; }

        /// <summary>
        /// Тип продукта: товар/материал
        /// </summary>
        public StockProductTypeEnum Type { get; set; }

        /// <summary>
        /// Тип товара - обычный товар, комплект, готовая продукция
        /// </summary>
        public StockProductSubTypeEnum ProductSubType { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Артикул
        /// </summary>
        public string Article { get; set; }

        /// <summary>
        /// Цена за единицу товара при продаже
        /// </summary>
        public decimal SalePrice { get; set; }

        /// <summary>
        /// Единица измерения
        /// </summary>
        public string UnitOfMeasurement { get; set; }

        /// <summary>
        /// Тип начисления НДС
        /// </summary>
        public NdsPositionType NdsPositionType { get; set; }

        /// <summary>
        /// Процент НДС
        /// </summary>
        public int NDS { get; set; }

        /// <summary>
        /// Использовать ставку НДС из Учетной политики
        /// </summary>
        public bool UseNdsFromAccPolicy { get; set; }

        /// <summary>
        /// Является ли товар комплектом
        /// </summary>
        public bool IsBundle { get; set; }

        /// <summary>
        /// Тип субконто
        /// </summary>
        /// <remarks>Функция - чтобы не сериализовать в json (и не передавать по сети)</remarks>
        public SubcontoType SubcontoType() => Common.Enums.Enums.Accounting.SubcontoType.Good;

        /// <summary>
        /// Имя субконто
        /// </summary>
        /// <remarks>Функция - чтобы не сериализовать в json (и не передавать по сети)</remarks>
        public string SubcontoName() => ProductName;

        /// <summary>
        /// Id связанного субконто
        /// </summary>
        public long? SubcontoId { get; set; }

        /// <summary>
        /// Признак "Маркированный"
        /// </summary>
        public bool IsLabeled { get; set; }

        /// <summary>
        /// Признак "Прослеживаемый"
        /// </summary>
        public bool IsTraceable { get; set; }

        /// <summary>
        /// Id кода вида товара
        /// </summary>
        public int? ProductTypeCodeId { get; set; }
    }
}