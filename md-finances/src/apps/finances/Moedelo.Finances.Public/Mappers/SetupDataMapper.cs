using System.Linq;
using Moedelo.Finances.Domain.Models;
using Moedelo.Finances.Public.ClientData;
using Moedelo.Finances.Public.ClientData.Setup;
using Moedelo.Finances.Public.Mappers.Integrations;

namespace Moedelo.Finances.Public.Mappers
{
    public static class SetupDataMapper
    {
        public static SetupDataClientData MapToClient(this SetupData setupData)
        {
            return new SetupDataClientData
            {
                RegistrationDate = setupData.RegistrationDate,
                LastClosedDate = setupData.LastClosedDate,
                BalanceDate = setupData.BalanceDate,
                AccessRuleFlags = MapToClientData(setupData.AccessRuleFlags),
                RegistrationInService = setupData.RegistrationInService,
                ImportMessages = setupData.ImportMessages,
                IntegrationErrors = setupData.IntegrationErrors.Select(x => x.Map()).ToList()
            };
        }

        private static AccessRuleFlagsClientData MapToClientData(AccessRuleFlags accessRuleFlags)
        {
            return new AccessRuleFlagsClientData
            {
                HasAccessToMoneyEdit = accessRuleFlags.HasAccessToMoneyEdit,
                HasAccessToPostings = accessRuleFlags.HasAccessToPostings,
                HasAccessToEditCurrencyOperations = accessRuleFlags.HasAccessToEditCurrencyOperations,
                HasAccessToViewNoAutoDeleteOperation = accessRuleFlags.HasAccessToViewNoAutoDeleteOperation
            };
        }
    }
}
