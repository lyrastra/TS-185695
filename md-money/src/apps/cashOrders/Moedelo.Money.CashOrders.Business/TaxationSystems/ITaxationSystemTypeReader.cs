using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.CashOrders.Business.TaxationSystems
{
    internal interface ITaxationSystemTypeReader
    {
        Task<TaxationSystemType?> GetByYearAsync(int year);
        Task<TaxationSystemType?> GetDefaultByYearAsync(int year);
    }
}
