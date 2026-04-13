using System.Threading.Tasks;

namespace Moedelo.Money.BankBalanceHistory.Business.Abstractions.ConsoleUser
{
    public interface IConsoleUserGetter
    {
        Task<int> GetConsoleUserId();
    }
}
