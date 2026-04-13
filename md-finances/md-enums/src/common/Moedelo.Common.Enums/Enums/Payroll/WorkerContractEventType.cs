using System.ComponentModel;

namespace Moedelo.Common.Enums.Enums.Payroll
{
    public enum WorkerContractEventType
    {
        [Description("Создание ГПД")]
        Create = 0,

        [Description("Обновление ГПД")]
        Update = 1,

        [Description("Удаление ГПД")]
        Delete = 2,
    }
}