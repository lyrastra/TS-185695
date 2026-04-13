using Moedelo.Money.Domain.TaxationSystems;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.TaxationSystems
{
    internal interface ITaxationSystemTypeReader
    {
        Task<TaxationSystemType?> GetByYearAsync(int year);
        Task<TaxationSystemType?> GetDefaultByYearAsync(int year);
        Task<TaxationSystem> GetFullByYearAsync(int year);
    }
}
