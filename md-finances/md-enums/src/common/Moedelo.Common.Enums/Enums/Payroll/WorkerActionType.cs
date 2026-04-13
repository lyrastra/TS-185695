using System.ComponentModel;

namespace Moedelo.Common.Enums.Enums.Payroll
{
    public enum WorkerActionType
    {
        [Description("Добавление сотрудника")]
        Addition = 0,

        [Description("Увольнение сотрудника")]
        Leave = 1,

        [Description("Удаление сотрудника")]
        Deletion = 2,

        [Description("Редактирование даты добавления сотрудника")]
        EditingAddDate = 3,

        [Description("Редактирование даты увольнения сотрудника")]
        EditingLeaveDate = 4,

        [Description("Восстановление сотрудника")]
        Recovery = 5
    }
}