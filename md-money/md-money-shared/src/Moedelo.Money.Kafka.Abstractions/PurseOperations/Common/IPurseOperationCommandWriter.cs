using Moedelo.Money.Kafka.Abstractions.PurseOperations.Common.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PurseOperations.Common
{
    public interface IPurseOperationCommandWriter
    {
        Task WriteCreateAsync(CreatePurseOperation commandData);
    }
}
