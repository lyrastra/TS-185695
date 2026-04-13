using System.Threading;
using Moedelo.Common.Enums.Enums.Finances.Money;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Models.Money.Table;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Threading.Tasks;

namespace Moedelo.Finances.Domain.Interfaces.Business.Money
{
    public interface IMoneyTableReader : IDI
    {
        Task<UnrecognizedMoneyTableResponse> GetUnrecognizedAsync(IUserContext userContext, MoneyTableRequest request,
            CancellationToken cancellationToken);
        Task<int> GetUnrecognizedOperationsCountAsync(IUserContext userContext, MoneySourceType sourceType,
            long? sourceId, CancellationToken cancellationToken);

        Task<ImportedMoneyTableResponse> GetImportedAsync(IUserContext userContext, MoneyTableRequest request,
            CancellationToken cancellationToken);
        Task<int> GetImportedCountAsync(IUserContext userContext, MoneySourceType sourceType, long? sourceId,
            CancellationToken ctx);

        Task<OutsourceProcessingMoneyTableResponse> GetOutsourceProcessingAsync(IUserContext userContext, OutsourceProcessingTableRequest request, CancellationToken ctx);
        Task<int> GetOutsourceProcessingCountAsync(IUserContext userContext, OutsourceProcessingTableRequest request, CancellationToken ctx);

        Task<MainMoneyMultiCurrencyTableResponse> GetMultiCurrencyTableAsync(IUserContext userContext,
            MainMoneyTableRequest request, CancellationToken cancellationToken);
    }
}