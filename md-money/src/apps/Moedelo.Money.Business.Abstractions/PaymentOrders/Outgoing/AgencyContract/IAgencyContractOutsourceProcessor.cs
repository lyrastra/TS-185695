using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.AgencyContract;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.AgencyContract;

public interface IAgencyContractOutsourceProcessor
{
    Task<OutsourceConfirmResult> ConfirmAsync(AgencyContractSaveRequest request);
}