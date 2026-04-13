using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.Commands.PurseOperations
{
    public interface IPurseOperationChangeTaxationSystemCommandWriter
    {
        Task WriteAsync(PurseOperationChangeTaxationSystemCommand command);
    }
}
