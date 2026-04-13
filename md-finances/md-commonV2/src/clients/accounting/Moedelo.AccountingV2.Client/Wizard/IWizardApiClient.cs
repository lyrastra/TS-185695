using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Wizard;

namespace Moedelo.AccountingV2.Client.Wizard
{
    public interface IWizardApiClient
    {
        Task<long> OpenAsync(int firmId, int userId, WizardType type, int period, int year);
    }
}
