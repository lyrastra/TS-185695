using Moedelo.Money.Domain.LinkedDocuments;

namespace Moedelo.Money.Domain.PaymentOrders.Incoming.IncomeFromCommissionAgent
{
    public class IncomeFromCommissionAgentLinks
    {
        public RemoteServiceResponse<ContractLink> Contract { get; set; }
    }
}