using Moedelo.AstralV3.Client.AstralInteractionsLogger;
using Moedelo.AstralV3.Client.AstralInteractionsLogger.Enums;
using Moedelo.AstralV3.Client.AstralInteractionsLogger.MessagesInspector;
using Moedelo.AstralV3.Client.Types;
using Moedelo.Common.Enums.Enums.ElectronicReports;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.AstralV3.Client.AstralClient
{
    /// <summary>
    /// Здесь лежат методы-обёртки, в которых мы логируем исключения
    /// </summary>
    public partial class AstralClient
    {
        /// <summary>
        /// Префикс для методов электронной отчётности в логах
        /// </summary>
        private readonly string EReportsPrefix = "EREP";

        public FunctionResult<FlcResult> CheckFiles(int? userId, int? firmId, List<FileObject> filesForChecking)
        {
            var inspector = new MessagesInspector(userId, firmId);
            return ExecuteWithLogging(() => CheckFilesInner(inspector, filesForChecking), nameof(CheckFiles), inspector);
        }

        public FunctionResult<string> ConfirmFile(int? userId, int? firmId, string sessionkey, string id)
        {
            var inspector = new MessagesInspector(userId, firmId);
            return ExecuteWithLogging(() => ConfirmFileInner(inspector, sessionkey, id), nameof(ConfirmFile), inspector);
        }

        public FunctionResult<string> ConfirmShipping(int? userId, int? firmId, string sessionkey, string uid, bool agree, string ipAddressOfSender = null)
        {
            var inspector = new MessagesInspector(userId, firmId);
            return ExecuteWithLogging(() => ConfirmShippingInner(inspector, sessionkey, uid, agree, ipAddressOfSender), nameof(ConfirmShipping), inspector);
        }

        public FunctionResult<string> DownloadCertificate(int? userId, int? firmId, string guid, string login, string password)
        {
            var inspector = new MessagesInspector(userId, firmId);
            return ExecuteWithLogging(() => DownloadCertificateInner(inspector, guid, login, password), nameof(DownloadCertificate), inspector);
        }

        public FunctionResult<FileObject[]> GetFilesByUid(int? userId, int? firmId, string sessionkey, string uid)
        {
            var inspector = new MessagesInspector(userId, firmId);
            return ExecuteWithLogging(() => GetFilesByUidInner(inspector, sessionkey, uid), nameof(GetFilesByUidInner), inspector);
        }

        public FunctionResult<FileObject> GetFilesWithUid(int? userId, int? firmId, string uid)
        {
            var inspector = new MessagesInspector(userId, firmId);
            return ExecuteWithLogging(() => GetFilesWithUidInner(inspector, uid), nameof(GetFilesWithUid), inspector);
        }

        public FunctionResult<UidWithListReceivers[]> GetNewUids(int? userId, int? firmId, string sessionKey)
        {
            var inspector = new MessagesInspector(userId, firmId);
            return ExecuteWithLogging(() => GetNewUidsInner(inspector, sessionKey), nameof(GetNewUids), inspector);
        }

        public FunctionResult<string> GetPfrRouting(int? userId, int? firmId)
        {
            var inspector = new MessagesInspector(userId, firmId);
            return ExecuteWithLogging(() => GetPfrRoutingInner(inspector), nameof(GetPfrRouting), inspector);
        }

        public FunctionResult<string> ReceiveRegistrationAnswer(int? userId, int? firmId, string data)
        {
            var inspector = new MessagesInspector(userId, firmId);
            return ExecuteWithLogging(() => ReceiveRegistrationAnswerInner(inspector, data), nameof(ReceiveRegistrationAnswer), inspector);
        }

        public FunctionResult<string> SendFiles(int? userId, int? firmId, string sessionKey, List<FileObject> files, FundType direction, string receiver, string xRealIp, string subject = null, string message = null)
        {
            var inspector = new MessagesInspector(userId, firmId);
            return ExecuteWithLogging(() => SendFilesInner(inspector, sessionKey, files, direction, receiver, xRealIp, subject, message), nameof(SendFiles), inspector);
        }

        public FunctionResult<string> SendFilesToFnsWithoutSms(int? userId, int? firmId, string sessionKey, List<FileObject> files, string receiver, string xRealIp, string abnGuid)
        {
            var inspector = new MessagesInspector(userId, firmId);
            return ExecuteWithLogging(() => SendFilesToFnsWithoutSmsInner(inspector, sessionKey, files, receiver, xRealIp, abnGuid), nameof(SendFilesToFnsWithoutSms), inspector);
        }

        public Task<FunctionResult<string>> SendRegistrationRequestAsync(int? userId, int? firmId, string data)
        {
            var inspector = new MessagesInspector(userId, firmId);
            return ExecuteWithLoggingAsync(() => SendRegistrationRequestAsyncInner(inspector, data), nameof(SendRegistrationRequestAsync), inspector);
        }

        /// <summary>
        /// Исполняет метод с логированием исключений
        /// </summary>
        private T ExecuteWithLogging<T>(Func<T> func, string methodId, IMessagesInspector inspector)
        {
            bool exceptionHappened = false;

            try
            {
                return func();
            }
            catch (Exception ex)
            {
                exceptionHappened = true;
                LogException(methodId, ex, inspector);
                throw;
            }
            finally
            {
                // Логирование нормальных запросов
                if (!exceptionHappened)
                {
                    LogSuccess(methodId, inspector);
                }
            }
        }

        /// <summary>
        /// Исполняет метод с логированием исключений (асинхронно)
        /// </summary>
        private async Task<T> ExecuteWithLoggingAsync<T>(Func<Task<T>> func, string methodId, IMessagesInspector inspector)
        {
            bool exceptionHappened = false;

            try
            {
                return await func().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                exceptionHappened = true;
                LogException(methodId, ex, inspector);
                throw;
            }
            finally
            {
                // Логирование нормальных запросов
                if (!exceptionHappened)
                {
                    LogSuccess(methodId, inspector);
                }
            }
        }

        private void LogException(string methodId, Exception ex, IMessagesInspector inspector)
        {
            LogEvent(methodId, EventType.Error, ex.ToString(), inspector);
        }

        private void LogSuccess(string methodId, IMessagesInspector inspector)
        {
            LogEvent(methodId, EventType.Info, null, inspector);
        }

        private void LogEvent(string methodId, EventType eventType, string exceptionMessage, IMessagesInspector inspector)
        {
            var eventToLog = new EventToLog()
            {
                MethodId = $"{EReportsPrefix}: {methodId}",
                EventType = eventType,
                ExceptionMessage = exceptionMessage,
                Request = inspector == null ? null : inspector.Request,
                Response = inspector == null ? null : inspector.Response,
                Duration = inspector == null ? TimeSpan.FromTicks(0) : inspector.Duration,
                UserId = inspector == null ? null : inspector.UserId,
                FirmId = inspector == null ? null : inspector.FirmId,
            };

            _logsWriter.Log(eventToLog);
        }
    }
}
