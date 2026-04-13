using System.Threading;
using System.Threading.Tasks;
using Moedelo.PaymentImport.Dto;

namespace Moedelo.PaymentImport.Client.MovementList.Storage
{
    public interface IMovementListStorageClient
    {
        /// <summary> Получение информации о 5 файлах выписки загурженных через интеграцию или в ручную</summary>
        /// <param name="firmId">Идентификатор организации</param>
        /// <param name="count">Количество последних файлов загруженых от текущей даты</param>
        /// <param name="cancellationToken">токен отмены операции</param>
        Task<MovementFileInfoDto[]> GetAsync(int firmId, int count = 5, CancellationToken cancellationToken = default);
    }
}