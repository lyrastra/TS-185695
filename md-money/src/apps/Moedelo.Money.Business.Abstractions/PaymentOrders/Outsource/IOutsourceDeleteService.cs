using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource;

public interface IOutsourceDeleteService
{
    public Task<OutsourceDeleteResult> DeleteAsync(long documentBaseId);
}