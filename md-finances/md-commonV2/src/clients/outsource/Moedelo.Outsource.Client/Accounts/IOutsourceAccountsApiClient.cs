using System.Threading.Tasks;
using Moedelo.Outsource.Dto.Accounts;

namespace Moedelo.Outsource.Client.Accounts;

/// <summary>
/// Агрегирующие аккаунты в "Бухобслуживании"
/// </summary>
public interface IOutsourceAccountsApiClient
{
    /// <summary>
    /// Создает аккаунт
    /// </summary>
    Task<int> CreateAsync(int firmId, int userId, AccountPostDto dto);
}