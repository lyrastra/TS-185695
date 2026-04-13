using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Domain.PaymentOrders.Incoming.RetailRevenue;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.RetailRevenue;

public interface IRetailRevenueOutsourceProcessor
{
    Task<OutsourceConfirmResult> ConfirmAsync(RetailRevenueSaveRequest request);
}