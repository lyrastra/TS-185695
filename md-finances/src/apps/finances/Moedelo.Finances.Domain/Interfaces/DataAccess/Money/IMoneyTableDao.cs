using Moedelo.Common.Enums.Enums.Finances.Money;
using Moedelo.Finances.Domain.Models.Money.Table;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Finances.Domain.Interfaces.DataAccess.Money
{
    public interface IMoneyTableDao
    {
        Task<UnrecognizedMoneyTableResponse> GetUnrecognizedAsync(int firmId, DateTime initialDate,
            MoneyTableRequest request, CancellationToken cancellationToken);
        Task<int> GetUnrecognizedOperationsCountAsync(int firmId, DateTime initialDate, MoneySourceType sourceType,
            long? sourceId, CancellationToken cancellationToken);

        Task<ImportedMoneyTableResponse> GetImportedAsync(int firmId, DateTime initialDate, MoneyTableRequest request,
            CancellationToken cancellationToken);
        Task<int> GetImportedCountAsync(int firmId, DateTime initialDate, MoneySourceType sourceType, long? sourceId);

        Task<OutsourceProcessingMoneyTableResponse> GetOutsourceProcessingAsync(int firmId, DateTime initialDate,
            OutsourceProcessingTableRequest request, CancellationToken ctx);
        Task<int> GetOutsourceProcessingCountAsync(int firmId, DateTime initialDate, OutsourceProcessingTableRequest request);

        Task<MainMoneyMultiCurrencyTableResponse> GetMultiCurrencyTableAsync(int firmId, DateTime initialDate,
            DateTime initialSummaryDate,
            MainMoneyTableRequest request, CancellationToken cancellationToken);
    }
}