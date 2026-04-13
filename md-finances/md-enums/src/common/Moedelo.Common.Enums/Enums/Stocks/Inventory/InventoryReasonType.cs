using System.ComponentModel;

namespace Moedelo.Common.Enums.Enums.Stocks.Inventory
{
    public enum InventoryReasonType
    {
        [Description("Контрольная проверка")]
        ControlAudit = 1,

        [Description("Смена МОЛ")]
        ChangeResponsiblePerson = 2,

        [Description("Годовая инвентаризация")]
        YearInventory = 3,

        [Description("Иное")]
        Other = 4,
    }
}