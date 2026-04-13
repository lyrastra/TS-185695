using System.ComponentModel;

namespace Moedelo.Common.Enums.Enums.MoneyBalanceMaster
{
    public enum MasterStep
    {
        [Description("Общая информация")]
        Welcome = 1,

        [Description("Баланс по учредителям")]
        Founders = 2,

        [Description("Баланс кассы")]
        Cash = 3,

        [Description("Баланс расчётных счётов")]
        SettlementAccounts = 4,

        [Description("Остатки по электронным кошелькам")]
        Purse = 5,

        [Description("Баланс склада")]
        Store = 6,

        [Description("Баланс по займам и депозитам")]
        Loans = 7,

        [Description("Баланс по основным средствам")]
        FixedAssets = 8,

        [Description("Баланс по фондам")]
        Funds = 9,

        [Description("Баланс по подотчётным лицам")]
        Employee = 10,

        [Description("Баланс по выплатам физ. лицам")]
        Salary = 11,

        [Description("Расчёт с покупателями и заказчиками")]
        BuyersAndCustomers = 12,

        [Description("Расчёт с поставщиками и подрядчиками")]
        SuppliersAndContractors = 13,

        [Description("Расчет с прочими контрагентами")]
        OtherKontragents = 14
    }
}