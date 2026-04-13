namespace Moedelo.Stock.Enums
{
    public enum StockOperationParentTypeEnum
    {
        /// <summary> Оприходывание </summary>
        Debit = 100,
        
        /// <summary> Списание </summary>
        WriteOff = 200,

        /// <summary> Операции инвентаризации </summary>
        Inventory = 300,

        /// <summary> Операции перемещения </summary>
        Movement = 400,

        /// <summary> Ввод остатков </summary>
        InputRemains = 500,

        /// <summary>
        /// Комплектация
        /// </summary>
        Bundling = 600,
        
        /// <summary>
        /// Разукомплектация
        /// </summary>
        Unbundling = 700,

        /// <summary>
        /// Производство
        /// </summary>
        Manufacturing = 800,
        
        /// <summary>
        /// Укд
        /// </summary>
        Ukd = 900,

        /// <summary>
        /// Отчет комиссионера
        /// </summary>
        CommissionAgentReport = 1000
    }
}