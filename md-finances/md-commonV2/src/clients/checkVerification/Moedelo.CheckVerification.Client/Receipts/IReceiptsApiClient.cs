using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CheckVerification.Client.Receipts.Models;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.CheckVerification.Client.Receipts
{
    public interface IReceiptsApiClient : IDI
    {
        Task<ApiDataDto<ReceiptDto>> GetAsync(ReceiptRequestDto model);
    }
}