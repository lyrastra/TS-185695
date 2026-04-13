using System.Threading.Tasks;
using Moedelo.ErptV2.Dto.FlcNetCore;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.ErptV2.Client.FlcNetCore
{
    public interface IFlcNetCoreApiClient : IDI
    {
        /// <summary>
        /// Отправляет файлы на проверку ФЛК.
        /// </summary>
        /// <param name="request"> Несколько файлов отправить можно и нужно, если  эти файлы клиент собирается отправлять вместе.
        /// Например НДС с приложениями или бухбаланс. А вот НДС и "прибыль" или тем более НДС и СЗВ-М вместе отправлять нельзя.</param>
        /// <returns>Список ошибок при проверке</returns>
        Task<Response> CheckFilesAsync(Request request);
    }
}