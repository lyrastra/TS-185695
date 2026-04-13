using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Moedelo.Requisites.Enums.StockVisibility
{
    public enum StockVisibilitySwitchStatus
    {
        [Description("Успешно.")]
        Ok = 0,

        [Description("Переключение было меньше месяца назад.")]
        SwitchedLessThanMonthAgo = 1,

        [Description("Скрытие склада недоступно - у фирмы присутствует СНО отличная от УСН типа \"Доходы\".")]
        InvalidTaxationSystem = 2,

        [Description("Скрытие склада доступно только для ИП.")]
        InvalidFormOfOwnership = 3,

        [Description("Недостаточно прав для переключения видимости склада.")]
        NoAccessToStockSwitch = 4
    }

    public static class SwitchResultStatusExtension
    {
        public static string GetDescription(this StockVisibilitySwitchStatus value)
        {
            return value.GetType()
                .GetMember(value.ToString())
                .FirstOrDefault()
                ?.GetCustomAttribute<DescriptionAttribute>()
                ?.Description;
        }
    }
}
