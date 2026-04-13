using System.Threading.Tasks;
using Moedelo.Outsource.Dto.Services;

namespace Moedelo.Outsource.Client.Services;

/// <summary>
/// Работа с настраиваемым списком услуг в ВРМ
/// </summary>
public interface IOutsourceServicesApiClient
{
    /// <summary>
    /// Создание услуги
    /// </summary>
    Task<int> CreateAsync(int accountId, ServicePostDto dto);
}