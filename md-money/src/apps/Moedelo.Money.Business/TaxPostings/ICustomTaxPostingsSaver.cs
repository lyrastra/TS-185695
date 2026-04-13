using Moedelo.Money.Domain.TaxPostings;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.TaxPostings
{
    internal interface ICustomTaxPostingsSaver
    {
        Task OverwriteAsync(CustomTaxPostingsOverwriteRequest overwriteRequest);
    }
}
