using System.ComponentModel;

namespace Moedelo.Common.Enums.Enums.Payroll
{
    public enum WorkerPaymentType
    {
        [Description("Сотрудник")]
        Employee = 0,

        [Description("Подотчетное лицо")]
        AccountablePerson = 1,

        [Description("ГПД")]
        Contract = 2,

        [Description("Учредитель")]
        Dividends = 3,
        
        [Description("По зарплатному проекту")]
        SalaryProject = 4,

        [Description("ГПД по зарплатному проекту")]
        ContractBySalaryProject = 5,

        [Description("Дивиденды по зарплатному проекту")]
        DividendsBySalaryProject = 6
    }
}