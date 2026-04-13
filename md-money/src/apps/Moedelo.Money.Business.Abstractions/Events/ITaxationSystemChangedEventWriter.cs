using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.Events
{
    public interface ITaxationSystemChangedEventWriter
    {
        Task WriteAsync(TaxationSystemChangedEvent changedEvent);
    }
}
