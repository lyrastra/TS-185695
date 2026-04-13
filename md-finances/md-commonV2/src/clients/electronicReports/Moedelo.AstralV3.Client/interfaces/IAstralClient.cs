using Moedelo.AstralV3.Client.Types;
using Moedelo.Common.Enums.Enums.ElectronicReports;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.AstralV3.Client
{
    public interface IAstralClient : IDI
    {
        /// <summary> Отправка файлов в Астрал </summary>
        /// <param name="sessionKey"> Сессионный ключ </param>
        /// <param name="files"> Список файлов для отправки </param>
        /// <param name="direction"> Направление (фонд-получатель) </param>
        /// <param name="receiver"> Код органа </param>
        /// <param name="xRealIp">IP-адрес абонента</param>
        /// <param name="subject">Тема письма</param>
        /// <param name="message">Текст письма</param>
        /// <returns> Идентификатор документооборота </returns>
        FunctionResult<string> SendFiles(int? userId, int? firmId, string sessionKey, List<FileObject> files, FundType direction, string receiver, string xRealIp, string subject = null, string message = null);

        FunctionResult<string> SendFilesToFnsWithoutSms(int? userId, int? firmId, string sessionKey, List<FileObject> files, string receiver, string xRealIp, string abnGuid);

        /// <summary>Получение списка Уидов с ответами из органов</summary>
        /// <param name="sessionKey">Ключ сессии</param>
        /// <returns>Список Уидов</returns>
        FunctionResult<UidWithListReceivers[]> GetNewUids(int? userId, int? firmId, string sessionKey);

        /// <summary>Получение файлов по Уиду</summary>
        /// <param name="sessionkey">Ключ сессии</param>
        /// <param name="uid">Уид</param>
        /// <returns>Список файлов</returns>
        FunctionResult<FileObject[]> GetFilesByUid(int? userId, int? firmId, string sessionkey, string uid);

        FunctionResult<FileObject> GetFilesWithUid(int? userId, int? firmId, string uid);

        FunctionResult<string> ConfirmFile(int? userId, int? firmId, string sessionkey, string id);

        FunctionResult<string> ConfirmShipping(int? userId, int? firmId, string sessionkey, string uid, bool agree, string ipAddressOfSender = null);

        FunctionResult<FlcResult> CheckFiles(int? userId, int? firmId, List<FileObject> filesForChecking);

        /// <summary> Отправка в Астрал запроса на регистрацию абонента </summary>
        /// <param name="data"> Содержимое пакета заявки на регистрацию ЭЦП в BASE64 </param>
        /// <returns> True - в случае успешной отправки, False - в случае неудачной отправки </returns>
        Task<FunctionResult<string>> SendRegistrationRequestAsync(int? userId, int? firmId, string data);

        /// <summary> Получение из Астрала ответа на регистрацию абонента </summary>
        /// <param name="data"> Идентификатор документооборота </param>
        /// <returns> Содержимое файла сертификата </returns>
        FunctionResult<string> ReceiveRegistrationAnswer(int? userId, int? firmId, string data);

        /// <summary> Получение списка роутинга ПФР </summary>
        /// <returns> Список роутинга ПФР в формате XML </returns>
        FunctionResult<string> GetPfrRouting(int? userId, int? firmId);

        /// <summary>Получить сертификат пользователя</summary>
        /// <param name="productGuid">Идентификатор карточки абонента в Астрале</param>
        /// <param name="login">Партнерский логин</param>
        /// <param name="pass">Партнерский пароль</param>
        /// <returns>Pdf-сертификат в base64 внутри json-массива</returns>
        FunctionResult<string> DownloadCertificate(int? userId, int? firmId, string guid, string login, string password);

    }
}
