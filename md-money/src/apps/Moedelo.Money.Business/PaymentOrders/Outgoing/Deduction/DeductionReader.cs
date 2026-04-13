using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.Deduction;
using Moedelo.Money.Business.Kontragents;
using Moedelo.Money.Business.Payroll;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.Deduction;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.Deduction
{
    [InjectAsSingleton(typeof(IDeductionReader))]
    internal sealed class DeductionReader : IDeductionReader
    {
        private readonly DeductionApiClient apiClient;
        private readonly DeductionLinksGetter linksGetter;
        private readonly IPaymentOrderAccessor paymentOrderAccessor;
        private readonly IEmployeeReader employeeReader;
        private readonly IKontragentsReader kontragentsReader;

        public DeductionReader(
            DeductionApiClient apiClient,
            DeductionLinksGetter linksGetter,
            IPaymentOrderAccessor paymentOrderAccessor, 
            IEmployeeReader employeeReader, 
            IKontragentsReader kontragentsReader)
        {
            this.apiClient = apiClient;
            this.linksGetter = linksGetter;
            this.paymentOrderAccessor = paymentOrderAccessor;
            this.employeeReader = employeeReader;
            this.kontragentsReader = kontragentsReader;
        }

        public async Task<DeductionResponse> GetByBaseIdAsync(long documentBaseId)
        {
            var response = await apiClient.GetAsync(documentBaseId);
            if (response.Contractor != null)
            {
                var kontragent = await kontragentsReader.GetByIdAsync(response.Contractor.Id);
                response.Contractor.Form = (int?)kontragent?.Form;
            }
            var documents = await linksGetter.GetAsync(documentBaseId);
            if (response.DeductionWorkerId.HasValue)
            {
                var employee = await employeeReader.GetByIdAsync(response.DeductionWorkerId.Value);
                response.DeductionWorkerFio = employee?.Name;
            }

            response.Contract = documents.Contract;
            response.IsReadOnly = paymentOrderAccessor.IsReadOnly(response);
            
            return response;
        }
    }
}
