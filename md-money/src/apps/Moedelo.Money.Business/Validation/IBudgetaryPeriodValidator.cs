using Moedelo.Money.Domain.Operations;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Validation
{
    internal interface IBudgetaryPeriodValidator
    {
        Task ValidateAsync(BudgetaryPeriod period);
    }
}
