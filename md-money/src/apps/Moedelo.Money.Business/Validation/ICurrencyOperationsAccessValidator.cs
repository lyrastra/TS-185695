using System.Threading.Tasks;

namespace Moedelo.Money.Business.Validation
{
    internal interface ICurrencyOperationsAccessValidator
    {
        Task ValidateAsync();
    }
}