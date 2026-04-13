using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Validation
{
    internal interface IClosedPeriodValidator
    {
        Task ValidateAsync(DateTime date, string propName = null);
    }
}
