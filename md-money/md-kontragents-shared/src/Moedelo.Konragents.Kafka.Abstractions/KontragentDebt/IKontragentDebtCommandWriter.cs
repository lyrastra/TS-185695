using System.Threading.Tasks;
using Moedelo.Konragents.Kafka.Abstractions.KontragentDebt.Commands;

namespace Moedelo.Konragents.Kafka.Abstractions.KontragentDebt
{
    public interface IKontragentDebtCommandWriter
    {
        Task WriteRecalculationCommandAsync(KontragentDebtRecalculationCommand commandData);
    }
}