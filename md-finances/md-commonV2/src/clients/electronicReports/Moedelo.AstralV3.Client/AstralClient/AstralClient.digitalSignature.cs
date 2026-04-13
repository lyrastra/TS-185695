using Moedelo.AstralV3.Client.AstralInteractionsLogger.MessagesInspector;
using Moedelo.AstralV3.Client.Content;
using Moedelo.AstralV3.Client.Data;
using Moedelo.AstralV3.Client.Helpers;
using System;
using System.Threading.Tasks;

namespace Moedelo.AstralV3.Client.AstralClient
{
    /// <summary>
    /// Часть класса AstralClient, ответственная за электронную подпись
    /// </summary>
    public partial class AstralClient
    {
        private FunctionResult<string> SendRegistrationRequestInner(IMessagesInspector inspector, string data)
        {
            try
            {
                using (var client = _astralClientsFactory.CreateRegServiceSoapClient(inspector))
                {
                    return ProcessRegistrationRequest(client.SendPacket(data));
                }
            }
            catch (Exception e)
            {
                return new FunctionResult<string>(SimpleFunctionResult.Exception, null, e);
            }
        }

        /// <summary> Отправка в Астрал запроса на регистрацию абонента </summary>
        /// <param name="data"> Содержимое пакета заявки на регистрацию ЭЦП в BASE64 </param>
        /// <returns> True - в случае успешной отправки, False - в случае неудачной отправки </returns>
        private async Task<FunctionResult<string>> SendRegistrationRequestAsyncInner(IMessagesInspector inspector, string data)
        {
            try
            {
                using (var client = _astralClientsFactory.CreateRegServiceSoapClient(inspector))
                {
                    return ProcessRegistrationRequest(await client.SendPacketAsync(data).ConfigureAwait(false));
                }
            }
            catch (Exception e)
            {
                return new FunctionResult<string>(SimpleFunctionResult.Exception, null, e);
            }
        }

        /// <summary> Получение из Астрала ответа на регистрацию абонента </summary>
        /// <param name="data"> Идентификатор документооборота </param>
        /// <returns> Содержимое файла сертификата </returns>
        private FunctionResult<string> ReceiveRegistrationAnswerInner(IMessagesInspector inspector, string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                _logger.Error(_loggerTag, Resource.ErrorDataToResponse, extraData: new { AstralClientMessage = true });
                return new FunctionResult<string>(SimpleFunctionResult.InputParameterError);
            }

            using (var client = _astralClientsFactory.CreateRegServiceSoapClient(inspector))
            {
                var response = client.ReceivePacket(data);
                return HandleRegistrationResponse(Helper.ParseRegServiceStandartResponse(response));
            }
        }

        /// <summary> Получение списка роутинга ПФР </summary>
        /// <returns> Список роутинга ПФР в формате XML </returns>
        private FunctionResult<string> GetPfrRoutingInner(IMessagesInspector inspector)
        {
            using (var client = _astralClientsFactory.CreateRegServiceSoapClient(inspector))
            {
                var response = client.GetRoutePfrList();
                return HandleRegistrationResponse(Helper.ParseRegServiceStandartResponse(response));
            }
        }

        /// <summary>Получить сертификат пользователя</summary>
        /// <param name="productGuid">Идентификатор карточки абонента в Астрале</param>
        /// <param name="login">Партнерский логин</param>
        /// <param name="pass">Партнерский пароль</param>
        /// <returns>Pdf-сертификат в base64 внутри json-массива</returns>
        private FunctionResult<string> DownloadCertificateInner(IMessagesInspector inspector, string guid, string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                return new FunctionResult<string>(SimpleFunctionResult.InputParameterError, "Проблема подтверждения: отсутствует логин или пароль.");
            }

            if (string.IsNullOrEmpty(guid))
            {
                return new FunctionResult<string>(SimpleFunctionResult.InputParameterError, "Проблема подтверждения: абонент не определён.");
            }

            using (var client = _astralClientsFactory.CreateExternalServiceSoapClient(inspector))
            {
                var response = client.ExportCertPrintForm(guid, login, password);
                return ParseExternalServiceStandardResponse(response);
            }
        }

        #region Private

        /// <summary>
        /// Метод, объединяющий общий код для SendRegistrationRequest() и SendRegistrationRequestAsync()
        /// </summary>
        private FunctionResult<string> ProcessRegistrationRequest(string textResponse)
        {
            var textResult = Helper.ParseRegServiceStandartResponse(textResponse);
            return HandleRegistrationResponse(textResult);
        }

        private FunctionResult<string> HandleRegistrationResponse(ResponseTransfer<string> response)
        {
            switch (response.Result)
            {
                case Data.Enum.AnswerType.Success:
                    return new FunctionResult<string>(SimpleFunctionResult.True, response.Value);
                case Data.Enum.AnswerType.Error:
                    return new FunctionResult<string>(SimpleFunctionResult.False, response.Message);
                case Data.Enum.AnswerType.Exception:
                    return new FunctionResult<string>(SimpleFunctionResult.Exception, response.Message);
                default:
                    return new FunctionResult<string>(SimpleFunctionResult.False, response.Message);
            }
        }

        /// <summary>Парсинг ответа от External сервиса Астрала</summary>
        /// <param name="result">Текст ответа</param>
        /// <returns>Результат парсинга, текст ошибки в случае её прихода или файл ответа в случае парсинга ответа на запрос</returns>
        private static FunctionResult<string> ParseExternalServiceStandardResponse(string result)
        {
            if (!result.StartsWith("["))
            {
                return new FunctionResult<string>(SimpleFunctionResult.False, result.Substring(0, System.Math.Min(500, result.Length))); //ограничим длину сообщения до 500 символов.
            }

            return new FunctionResult<string>(SimpleFunctionResult.True, result, null);
        }

        #endregion
    }
}
