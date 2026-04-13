using System.Threading.Tasks;
using Moedelo.Outsource.Dto.PageAccess;

namespace Moedelo.Outsource.Client.PageAccess;

public interface IOutsourcePageAccessApiClient
{
    /// <summary>
    /// Обновляет настройки доступности страниц
    /// </summary>
    Task UpdateAsync(GroupSettingPostDto dto);
}