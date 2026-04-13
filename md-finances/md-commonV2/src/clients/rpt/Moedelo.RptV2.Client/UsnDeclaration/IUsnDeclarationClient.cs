using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.RptV2.Dto.UsnDeclaration;
using System.Threading.Tasks;

namespace Moedelo.RptV2.Client.UsnDeclaration
{
    public interface IUsnDeclarationClient : IDI
    {
        Task<UsnDeclarationDto> GetByIdAsync(int firmId, int userId, long id);

        Task<UsnDeclarationDto> GetByYearAsync(int firmId, int userId, int year);

        Task<UsnTaxProfitDataDto> GetUsnTaxProfitDataAsync(int firmId, int userId, int year, bool? hasEmployees);

        Task<UsnTaxProfitSumDto> GetUsnTaxProfitSumAsync(int firmId, int userId, UsnTaxProfitDataDto taxData);

        Task<UsnTaxProfitAndExpenseDataDto> GetUsnTaxProfitAndExpenseDataAsync(int firmId, int userId, int year);

        Task<UsnTaxProfitAndExpenseSumDto> GetUsnTaxProfitAndExpenseSumAsync(int firmId, int userId,
            UsnTaxProfitAndExpenseDataDto taxData);

        Task<BankPaymentDto> GetBankPaymentAsync(int firmId, int userId, long id);
    }
}
