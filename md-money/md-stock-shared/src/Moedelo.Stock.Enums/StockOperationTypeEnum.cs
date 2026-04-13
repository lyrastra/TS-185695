using System.ComponentModel;

namespace Moedelo.Stock.Enums
{
    public enum StockOperationTypeEnum
    {
        /// <summary>
        /// Безвозмездное получение
        /// </summary>
        DebitGratuitous = 103,

        /// <summary>
        /// Поступление в уставной капитал
        /// </summary>
        DebitCharterCapital = 104,

        /// <summary>
        /// Поступление без документов
        /// </summary>
        DebitWithoutDocs = 106,

        /// <summary> 
        /// Возврат товара от покупателя
        /// </summary>
        Refund = 107,

        /// <summary>
        /// Покупка (Поступление с документами)
        /// </summary>
        DebitWithDocs = 108,

        /// <summary>
        /// Поступление товаров по агентскому договору или договору комиссии
        /// </summary>
        DebitAgent = 109,

        /// <summary>
        /// Безвозмездное списание
        /// </summary>
        WriteOffGrauitous = 202,

        /// <summary>
        /// Передача в уставный капитал
        /// </summary>
        WriteOffCharterCapital = 207,

        /// <summary>
        /// Продажа
        /// </summary>
        WriteOffSale = 208,

        /// <summary>
        /// Передача в переработку
        /// </summary>
        WriteOffRecycling = 206,
        
        /// <summary> 
        /// Требование-накладная 
        /// </summary>
        RequisitionWaybill = 209,
        
        /// <summary> 
        /// Списание товара по посреднической схеме (я - посредник) 
        /// </summary>
        WriteOffMediation = 210,
        
        /// <summary>
        /// Операции инвентаризации
        /// </summary>
        [Description("Инвентаризация")]
        Inventory = 300,

        /// <summary>
        /// Инвентаризация-излишки
        /// </summary>
        [Description("Инвентаризация, излишек")]
        PositiveInventory = 301,

        /// <summary>
        /// Инвентаризация-недостача
        /// </summary>
        [Description("Инвентаризация, недостача")]
        NegativeInventory = 302,

        // Операции перемещения
        Movement = 400,
        
        /// <summary>
        /// Перемещение на склад
        /// </summary>
        MovementToStock = 401,

        /// <summary>
        /// Перемещение со склада
        /// </summary>
        MovementFromStock = 402,

        /// <summary>
        /// Ввод остатков
        /// </summary>
        InputRemains = 500,

        /// <summary>
        /// Операции комплектации
        /// </summary>
        Bundling = 600,
        
        /// <summary>
        /// Комплектация: создание номенклатуры-комплекта
        /// </summary>
        BundlingDebit = 601,

        /// <summary>
        /// Комплектация: списание номенклатур-составляющих
        /// </summary>
        BundlingWriteOff = 602,

        /// <summary>
        /// Операции разукомплектации
        /// </summary>
        Unbundling = 700,

        /// <summary>
        /// Разукомплектация: списание номенклатуры-комплекта
        /// </summary>
        UnbundlingWriteOff = 701,

        /// <summary>
        /// Разукомплектация: создание номенклатур-составляющих
        /// </summary>
        UnbundlingDebit = 702,

        /// <summary>
        /// Производство
        /// </summary>
        Manufacturing = 800,

        /// <summary>
        /// Производство: создание номенклатур-готовых товаров
        /// </summary>
        ManufacturingDebit = 801,

        /// <summary>
        /// Производство: списание основного сырья
        /// </summary>
        ManufacturingWriteOff = 802,

        /// Производство: списание иного сырья
        /// </summary>
        ManufacturingWriteOffOther = 803,

        /// <summary>
        /// УКД
        /// </summary>
        Ukd = 900,
        
        /// <summary>
        /// УКД: возврат от покупателя
        /// </summary>
        UkdRefund = 901,

        /// <summary>
        /// Отчет комиссионера
        /// </summary>
        CommissionAgentReport = 1000,

        /// <summary>
        /// Отчет комиссионера: продажа
        /// </summary>
        CommissionAgentReportWriteOffSale = 1001,

        /// <summary>
        /// Отчет комиссионера: возврат
        /// </summary>
        CommissionAgentReportRefund = 1002,
    }
}