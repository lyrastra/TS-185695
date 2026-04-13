using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Validation
{
    internal interface ICashOrdersValidator
    {
        Task ValidateAsync(long documentBaseId, OperationType? operationType = null, MoneyDirection? direction = null);
    }
}
