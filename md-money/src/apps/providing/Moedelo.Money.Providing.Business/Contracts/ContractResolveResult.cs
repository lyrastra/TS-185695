namespace Moedelo.Money.Providing.Business.Contracts;

internal sealed class ContractResolveResult
{
    private ContractResolveResult(bool isSuccess, Contract contract)
    {
        IsSuccess = isSuccess;
        Contract = contract;
    }

    public bool IsSuccess { get; }

    public Contract Contract { get; }

    public static ContractResolveResult Success(Contract contract) => new(true, contract);

    public static ContractResolveResult Fail() => new(false, null);
}