using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Finances.Domain.Enums.Money;
using Moedelo.Finances.Domain.Interfaces.Business.Payment;
using Moedelo.Finances.Domain.Interfaces.DataAccess.Money.Operations;
using Moedelo.Finances.Domain.Models.Money.Duplicates;
using Moedelo.Finances.Domain.Models.Money.Operations.Duplicates;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;

namespace Moedelo.Finances.Business.Services.PaymentOrders
{
    [InjectAsSingleton]
    public class PaymentOrdersDuplicatesReader : IPaymentOrdersDuplicatesReader
    {
        private readonly IPaymentOrdersDuplicatesDao dao;

        public PaymentOrdersDuplicatesReader(IPaymentOrdersDuplicatesDao dao)
        {
            this.dao = dao;
        }

        public Task<int?> GetIncomingPaymentOrderIdAsync(DuplicateOperationRequest request)
        {
            request.Direction = (int)MoneyDirection.Incoming;
            return dao.GetPaymentOrderIdAsync(request);
        }

        public Task<int?> GetOutgoingPaymentOrderIdAsync(DuplicateOperationRequest request)
        {
            request.Direction = (int)MoneyDirection.Outgoing;
            return dao.GetPaymentOrderIdAsync(request);
        }

        public Task<List<OperationDuplicate>> GetAllPaymentOrdersAsync(DuplicateOperationRequest request, MoneyDirection direction)
        {
            request.Direction = (int)direction;
            return dao.GetAllPaymentOrdersAsync(request);
        }
    }
}