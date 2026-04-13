using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.TaxationSystems
{
    internal interface ITaxationSystemTypeReader
    {
        Task<TaxationSystemType?> GetByYearAsync(int year);
        Task<TaxationSystemType?> GetDefaultByYearAsync(int year);
    }
}
