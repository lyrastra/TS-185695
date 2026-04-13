using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.BillChanges.Dto;

namespace Moedelo.Billing.Abstractions.BillChanges;

public interface IBillChangesApiClient
{
    /// <summary>
    /// Проверка возможности изменения ОПФ для счетов фирмы
    /// </summary>
    /// <param name="requestDto">Параметры запроса</param>
    /// <returns></returns>
    Task<BillsCanChangeResponseDto> CanChangeBillLegalTypeAsync(
        BillsChangeLegalTypeRequestDto requestDto);

    /// <summary>
    /// Проверка возможности изменения СНО для счетов фирмы
    /// </summary>
    /// <param name="requestDto"></param>
    /// <returns></returns>
    Task<BillsCanChangeResponseDto> CanChangeBillTaxationSystemTypeAsync(
        BillChangeTaxationSystemRequestDto requestDto);

    /// <summary>
    /// Инициализация смены ОПФ в счетах фирмы
    /// </summary>
    /// <param name="requestDto"></param>
    /// <returns></returns>
    Task ChangeBillLegalTypeAsync(BillsChangeLegalTypeRequestDto requestDto);

    /// <summary>
    /// Инициализация смены СНО в счетах фирмы
    /// </summary>
    /// <param name="requestDto"></param>
    /// <returns></returns>
    Task ChangeBillTaxationSystemTypeAsync(BillChangeTaxationSystemRequestDto requestDto);

}
