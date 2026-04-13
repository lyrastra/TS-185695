using System.Threading.Tasks;

namespace Moedelo.Money.Business.TaxPostings
{
    public interface ITaxPostingsRemover
    {
        Task DeletePatentPostingsAsync(long documentBaseId);
    }
}