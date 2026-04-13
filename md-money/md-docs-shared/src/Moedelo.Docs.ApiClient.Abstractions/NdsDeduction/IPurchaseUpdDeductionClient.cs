using System.Threading.Tasks;
using Moedelo.Docs.ApiClient.Abstractions.NdsDeduction.Models;

namespace Moedelo.Docs.ApiClient.Abstractions.NdsDeduction
{
    public interface IPurchaseUpdDeductionClient
    {
        Task SaveDeductionsAsync(SaveDeductionDto saveDeductionSaveRequest);
    }
}