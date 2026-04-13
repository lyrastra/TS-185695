using System.ComponentModel;

namespace Moedelo.Common.Enums.Enums.Payroll
{
    public enum EfsChildCareEventType
    {
        [Description("Добавление декретного отпуска или отпуска по уходу")]
        Create = 0,

        [Description("Обновление декретного отпуска или отпуска по уходу")]
        Update = 1,

        [Description("Удаление декретного отпуска или отпуска по уходу")]
        Delete = 2
    }
}