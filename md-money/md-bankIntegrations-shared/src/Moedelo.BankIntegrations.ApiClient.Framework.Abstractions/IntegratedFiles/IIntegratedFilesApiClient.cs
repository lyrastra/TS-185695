using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.IntegratedFile;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Framework.Abstractions.IntegratedFiles
{
    public interface IIntegratedFilesApiClient
    {
        /// <summary>
        /// Получить файл выписки по идентификатору
        /// </summary>
        /// <param name="fileId">идентификатор файла</param>
        /// <param name="firmId">идентификатор фирмы, которой принадлежит файл</param>
        /// <returns>файл выписки</returns>
        Task<IntegratedFileDto> GetAsync(int fileId, int firmId);

        /// <summary>
        /// Получить список необработанных файлов выписки для указанной фирмы
        /// </summary>
        /// <param name="firmId">идентификатор фирмы</param>
        /// <returns>список файлов выписок</returns>
        Task<IntegratedFileDto[]> GetNotProcessedAsync(int firmId);

        /// <summary>
        /// Получить список необработанных файлов выписки для указанной фирмы по указанному партнёру-интегратору
        /// </summary>
        /// <param name="firmId">идентификатор фирмы</param>
        /// <param name="integratorId">идентификатор партнёра-интегратора</param>
        /// <returns>список файлов выписок</returns>
        Task<IntegratedFileDto[]> GetNotProcessedAsync(int firmId, IntegrationPartners integratorId);

        /// <summary>
        /// Получить последний необработанный файл выписки для указанной фирмы по указанному партнёру-интегратору
        /// </summary>
        /// <param name="firmId">идентификатор фирмы</param>
        /// <param name="integratorId">идентификатор партнёра-интегратора</param>
        /// <returns>файл выписки</returns>
        Task<IntegratedFileDto> GetLastNotProcessedAsync(int firmId, IntegrationPartners integratorId);

        /// <summary>
        /// Обновить значение содержимого файла выписки 
        /// </summary>
        /// <param name="fileId">идентификатор файла выписки</param>
        /// <param name="firmId">идентификатор фирмы</param>
        /// <param name="fileText">новое содержимое файла</param>
        /// <returns>идентификатор обновленного файла. равен fileId, если файл найдён. иначе равен 0</returns>
        Task UpdateFileContentAsync(int fileId, int firmId, string fileText);

        /// <summary>
        /// Установить значение свойства "Был обновлён" файла выписки 
        /// </summary>
        /// <param name="fileId">идентификатор файла выписки</param>
        /// <param name="firmId">идентификатор фирмы</param>
        /// <returns>идентификатор обновленного файла. равен fileId, если файл найдён. иначе равен 0</returns>
        Task SetIsAddedAsync(int fileId, int firmId);

        /// <summary>
        /// Установить значение свойства "Был пропущен" файла выписки 
        /// </summary>
        /// <param name="fileId">идентификатор файла выписки</param>
        /// <param name="firmId">идентификатор фирмы</param>
        /// <returns>идентификатор обновленного файла. равен fileId, если файл найдён. иначе равен 0</returns>
        Task SetIsSkippedAsync(int fileId, int firmId);

        /// <summary>
        /// Получить количество необработанных непустых файлов выписок для заданной фирмы
        /// </summary>
        /// <param name="firmId">идентификатор фирмы</param>
        /// <returns>количество файлов выписок</returns>
        Task<int> CountNotProcessedNotEmptyAsync(int firmId);

        /// <summary>
        /// Создать файлы выписок
        /// </summary>
        /// <param name="files">Заявки на создание файлов выписок</param>
        /// <returns>список выданных идентификаторов (в том же порядке, в котором размещены заявки на создание files)</returns>
        Task<IReadOnlyCollection<int>> SaveAsync(IReadOnlyCollection<IntegratedFileCreationRequestDto> files);

        /// <summary>
        /// Посчитать количество файлов выписок с незаполненным полем IntegrationRequestId за указанную дату
        /// </summary>
        /// <param name="date">Дата, на которую надо посчитать файлы</param>
        /// <returns>количество файлов выписок  с незаполненным полем IntegrationRequestId за указанную дату</returns>
        Task<int> CountDailyFilesWithEmptyIntegrationRequestIdAsync(DateTime date);
    }
}
