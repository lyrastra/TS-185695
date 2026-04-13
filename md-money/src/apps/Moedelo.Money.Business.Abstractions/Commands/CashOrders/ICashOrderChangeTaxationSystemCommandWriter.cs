using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.Commands.CashOrders
{
    public interface ICashOrderChangeTaxationSystemCommandWriter
    {
        Task WriteAsync(CashOrderChangeTaxationSystemCommand command);
    }
}
