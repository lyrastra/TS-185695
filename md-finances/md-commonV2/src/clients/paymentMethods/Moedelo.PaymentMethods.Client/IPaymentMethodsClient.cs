using Moedelo.PaymentMethods.Dto;
using System.Threading.Tasks;

namespace Moedelo.PaymentMethods.Client
{
    public interface IPaymentMethodsClient
    {
        Task<PaymentMethodsRangeDto> GetFilteredRangeAsync(GetFilteredRangeRequestDto request);

        Task UpdateAsync(UpdatePaymentMethodDto dto);

        Task<CreatePaymentMethodReponseDto> CreateAsync(CreatePaymentMethodDto dto);

        Task<int> GetNextIdAsync();

        Task<PaymentMethodDto[]> GetAllAsync();

        Task<PaymentMethodDto[]> GetByCriteriaAsync(PaymentMethodSearchCriteriaDto dto);

        Task<PaymentMethodDto[]> GetCodeAutocompleteAsync(string code);

        Task DisableAsync(string code);

        Task EnableAsync(string code);
    }
}