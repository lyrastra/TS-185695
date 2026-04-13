using System.Threading.Tasks;

namespace Moedelo.Money.Business.Validation
{
    internal interface ICashValidator
    {
        Task ValidateAsync(long cashId);
    }
}
