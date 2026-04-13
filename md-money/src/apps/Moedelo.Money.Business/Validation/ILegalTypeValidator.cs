using System.Threading.Tasks;

namespace Moedelo.Money.Business.Validation
{
    internal interface ILegalTypeValidator
    {
        Task ValidateForIpAsync();
        Task ValidateForUlAsync();
    }
}
