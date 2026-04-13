using Moedelo.Money.Domain.TaxPostings;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.TaxPostings
{
    public interface ITaxPostingReader
    {
        Task<TaxPostingsData> GetByBaseIdAsync(long baseId);
    }
}
