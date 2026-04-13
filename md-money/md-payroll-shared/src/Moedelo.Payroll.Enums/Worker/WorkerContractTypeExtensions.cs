namespace Moedelo.Payroll.Shared.Enums.Worker;

public static class WorkerContractTypeExtensions
{
    public static bool IsExpressType(this WorkerContractType type)
    {
        return type is WorkerContractType.ExpressUpTo6Months or WorkerContractType.ExpressMore6Months
            or WorkerContractType.PartTimeUpTo6Months or WorkerContractType.PartTimeMore6Months;
    }
}