using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.StockDebitOperation;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AccountingV2.Client.StockDebitOperation
{
    public interface IStockDebitOperationClient : IDI
    {
        Task<AccountingPostingsByDocumentDto> GetPostingsAsync(int firmId, int userId, StockOperationDto dto);
    }
}