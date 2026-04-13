using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Kafka.PaymentOrders.Outgoing.BankFee.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.BankFee
{
    public interface IBankFeeCommandWriter : IDI
    {
        Task WriteImportAsync(string key, string token, ImportBankFee commandData);
        Task WriteImportDuplicateAsync(string key, string token, ImportDuplicateBankFee commandData);
    }
}