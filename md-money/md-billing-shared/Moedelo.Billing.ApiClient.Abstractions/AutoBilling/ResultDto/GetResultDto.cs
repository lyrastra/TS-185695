namespace Moedelo.Billing.Abstractions.AutoBilling.ResultDto;

public class GetResultDto<T>
{
    public int Total { get; set; }
    public T[] Items { get; set; }
}