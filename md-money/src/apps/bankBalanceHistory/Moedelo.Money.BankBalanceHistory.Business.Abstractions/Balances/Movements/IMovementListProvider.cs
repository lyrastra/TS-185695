using Moedelo.Parsers.Klto1CParser.Models.Klto1CParser;
using System.Threading.Tasks;

namespace Moedelo.Money.BankBalanceHistory.Business.Abstractions.Balances.Movements
{
    public interface IMovementListProvider
    {
        Task<MovementList> GetByFileIdAsync(string fileId);
    }
}
