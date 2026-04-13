using System;
using System.Threading.Tasks;
using Moedelo.Money.Domain.PaymentOrders.Incoming.RetailRevenue;

namespace Moedelo.Money.Business.Validation;

internal interface IDateLessFirmRegistrationValidator
{
    Task ValidateAsync(DateTime date, string propName = null);
}