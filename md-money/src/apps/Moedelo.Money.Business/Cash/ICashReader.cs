using System.Threading.Tasks;

namespace Moedelo.Money.Business.Cash
{
    internal interface ICashReader
    {
        Task<CashModel> GetByIdAsync(long cashId);
    }
}
