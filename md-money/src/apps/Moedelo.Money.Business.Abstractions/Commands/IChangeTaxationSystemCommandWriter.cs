using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.Commands
{
    public interface IChangeTaxationSystemCommandWriter
    {
        Task WriteAsync(ChangeTaxationSystemCommand command);
    }
}
