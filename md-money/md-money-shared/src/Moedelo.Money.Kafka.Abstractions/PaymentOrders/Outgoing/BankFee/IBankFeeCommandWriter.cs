using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.BankFee.Commands;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.BankFee
{
    public interface IBankFeeCommandWriter
    {
        Task WriteImportAsync(ImportBankFee commandData);

        Task WriteImportDuplicateAsync(ImportDuplicateBankFee commandData);
    }
}