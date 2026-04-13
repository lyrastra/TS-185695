using System;

namespace Moedelo.Payroll.Shared.Enums.Worker
{
    public enum WorkerContractType
    {
        [WorkerContractType("Бессрочный ТД", "0")]
        Unlimited = 0,

        [WorkerContractType("Срочный ТД до 6 мес.", "0.1")]
        ExpressUpTo6Months,

        [WorkerContractType("Срочный ТД более 6 мес.", "0.2")]
        ExpressMore6Months,

        [WorkerContractType("ТД по совместительству", "1")]
        PartTime,

        [WorkerContractType("ТД по совместительству до 6 мес.", "1.1")]
        PartTimeUpTo6Months,

        [WorkerContractType("ТД по совместительству более 6 мес.", "1.2")]
        PartTimeMore6Months
    }

    public class WorkerContractTypeAttribute : Attribute
    {
        public string Description { get; }
        
        public string Value { get; }

        public WorkerContractTypeAttribute(string description, string value)
        {
            Description = description;
            Value = value;
        }
    }
}