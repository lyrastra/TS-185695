using System.Threading.Tasks;
using Moedelo.Outsource.Dto.AccountRule;

namespace Moedelo.Outsource.Client.AccountRule;

/// <summary>
/// Агрегирующие аккаунты в "Бухобслуживании" - настраиваемый список прав
/// </summary>
public interface IOutsourceAccountRulesApiClient
{
    /// <summary>
    /// Добавляет права, доступные для редактирования аккаунту
    /// </summary>
    Task AddRulesAsync(int firmId, int userId, int accountId, AccountRulesPutDto dto);
}