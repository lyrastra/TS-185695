using System;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Stocks;
using Moedelo.StockV2.Dto.Settings;

namespace Moedelo.StockV2.Client.Settings;

public interface IStockSettingsClient
{
    Task<bool> IsStockEnabledAsync(int firmId, int userId);
    Task<StockSettingsDto> GetAsync(int firmId, int userId);
    Task<StockSettingsDto> EnableAsync(int firmId, int userId, DateTime stockActivationDate);
    Task<StockDisableResponseStatusCode> DisableAsync(int firmId, int userId);
}