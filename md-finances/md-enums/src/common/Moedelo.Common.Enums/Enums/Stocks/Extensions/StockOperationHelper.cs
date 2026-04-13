namespace Moedelo.Common.Enums.Enums.Stocks.Extensions
{
    public static class StockOperationHelper
    {
        /// <summary>
        /// Для данного типа операции нужен расчет себестоимости
        /// </summary>
        public static bool HasSelfCost(StockOperationParentTypeEnum parentCode, StockOperationTypeEnum code)
        {
            return parentCode == StockOperationParentTypeEnum.WriteOff ||
                   parentCode == StockOperationParentTypeEnum.Movement ||
                   parentCode == StockOperationParentTypeEnum.Bundling ||
                   parentCode == StockOperationParentTypeEnum.Unbundling ||
                   parentCode == StockOperationParentTypeEnum.Manufacturing ||
                   (parentCode == StockOperationParentTypeEnum.Inventory && code != StockOperationTypeEnum.PositiveInventory) ||
                   code == StockOperationTypeEnum.Refund ||
                   parentCode == StockOperationParentTypeEnum.Ukd ||
                   parentCode == StockOperationParentTypeEnum.CommissionAgentReport;
        }
    }
}