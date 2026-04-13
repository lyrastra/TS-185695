using System.Threading.Tasks;

namespace Moedelo.AccountingV2.Client.BalanceAndIncome
{
    public interface IAccountingOfBalanceAndIncomeWizardApiClient
    {
        Task CreatePostings(int firmId, int userId, long wizardStateId, bool ignoreWizardState = false);
    }
}
