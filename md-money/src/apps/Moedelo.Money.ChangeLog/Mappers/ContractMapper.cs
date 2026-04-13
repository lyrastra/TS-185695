using Moedelo.Contracts.ApiClient.Abstractions.legacy.Dtos;

namespace Moedelo.Money.ChangeLog.Mappers
{
    static class ContractMapper
    {
        public static string MapToName(this ContractDto contract, long? contractBaseId)
        {
            if (contractBaseId == null)
            {
                return null;
            }
            return contract switch
            {
                null => $"Неизвестный договор #{contractBaseId.Value} (тех.идентификатор)",
                _ when contract.IsMainContract => "Основной договор",
                _ => $"Договор №{contract.Number} от {contract.Date:dd.MM.yyyy}"
            };
        }
    }
}
