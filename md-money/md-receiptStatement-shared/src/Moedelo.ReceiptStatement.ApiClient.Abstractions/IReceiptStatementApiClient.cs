using Moedelo.ReceiptStatement.ApiClient.Abstractions.Dto;
using System.Threading.Tasks;

namespace Moedelo.ReceiptStatement.ApiClient.Abstractions
{
    public interface IReceiptStatementApiClient
    {
        Task<ReceiptStatementDto> GetByBaseIdAsync(long documentBaseId);
    }
}
