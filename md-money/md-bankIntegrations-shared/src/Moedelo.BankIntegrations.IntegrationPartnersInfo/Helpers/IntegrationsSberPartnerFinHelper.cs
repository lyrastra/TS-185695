using System.Collections.Generic;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.IntegrationPartnersInfo.Helpers;

public static class IntegrationsSberPartnerFinHelper
{
    public const string BlockSberIntegration = "BlockSberIntegration";
    public const IntegrationPartners Partner = IntegrationPartners.SberBank;
    
    /// <summary>
    /// Если у клиента неоплаченный тариф финансиста для Сбер, то нужно удалить Сбер из списка доступных интеграций
    /// </summary>
    /// <param name="firmFlags">Список всех флагов клиента</param>
    /// <returns></returns>
    public static bool NeedRemoveAccessibleIfUnpaidFinancierSberbankTariff(
        Dictionary<string, bool> firmFlags)
    {
        firmFlags.TryGetValue(BlockSberIntegration, out bool blocked);
        return blocked;
    }
}