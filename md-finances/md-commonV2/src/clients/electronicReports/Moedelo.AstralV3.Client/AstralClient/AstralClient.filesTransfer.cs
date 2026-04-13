using Moedelo.AstralV3.Client.AstralInteractionsLogger.MessagesInspector;
using Moedelo.AstralV3.Client.AstralTransferService;
using Moedelo.AstralV3.Client.Helpers;
using Moedelo.AstralV3.Client.Types;
using Moedelo.Common.Enums.Enums.ElectronicReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.AstralV3.Client.BankArchiveService;

namespace Moedelo.AstralV3.Client.AstralClient
{
    /// <summary>
    /// Часть класса AstralClient, ответственная за передачу файлов
    /// </summary>
    public partial class AstralClient
    {
        private FunctionResult<string> SendFilesToFnsWithoutSmsInner(IMessagesInspector inspector, string sessionKey, List<FileObject> files, string receiver, string xRealIp, string abnGuid)
        {
            var request = new SendReviseFNS
            {
                Files = AstralMapper.MapFiles(files),
                ReceiverFNS = receiver,
                XRealIP = xRealIp,
                GUID = abnGuid
            };

            using (var serviceClient = _astralClientsFactory.CreateWorkflowServiceClient(inspector))
            {
                var answer = serviceClient.SendReviseFNS(new AstralTransferService.RequestHeader { sessionkey = sessionKey }, request);
                return new FunctionResult<string>(SimpleFunctionResult.True, answer.UID);
            }
        }

        /// <summary> Отправка файлов в Астрал </summary>
        /// <param name="sessionKey"> Сессионный ключ </param>
        /// <param name="files"> Список файлов для отправки </param>
        /// <param name="direction"> Направление (фонд-получатель) </param>
        /// <param name="receiver"> Код органа </param>
        /// <param name="xRealIp">IP-адрес абонента</param>
        /// <param name="subject">Тема письма</param>
        /// <param name="message">Текст письма</param>
        /// <returns> Идентификатор документооборота </returns>
        private FunctionResult<string> SendFilesInner(
            IMessagesInspector inspector, 
            string sessionKey, 
            List<FileObject> files, 
            FundType direction, 
            string receiver, 
            string xRealIp, 
            string subject = null, 
            string message = null)
        {
            if (string.IsNullOrEmpty(sessionKey) || !files.Any() || !Enum.IsDefined(typeof(FundType), direction) || string.IsNullOrEmpty(receiver))
            {
                _logger.Error(_loggerTag, "Not enough data", extraData: new { AstralClientMessage = true });
                return new FunctionResult<string>(SimpleFunctionResult.InputParameterError);
            }

            if ((direction == FundType.PFR || direction == FundType.PfrNeformal) && receiver.Length == 6)
            {
                receiver = string.Format("{0}-{1}", receiver.Substring(0, 3), receiver.Substring(3));
            }

            switch (direction)
            {
                case FundType.FNS:
                case FundType.FnsIon:
                    // Формально
                    return SendFilesFNS(inspector, sessionKey, files, receiver, xRealIp, subject, message, isLetter: false);

                case FundType.FnsNeformal:
                    // Неформально
                    return SendFilesFNS(inspector, sessionKey, files, receiver, xRealIp, subject, message, isLetter: true);

                case FundType.FSS:
                    return SendFilesFSS(inspector, sessionKey, files);

                case FundType.PFR when receiver == "00":
                    return SendFilesAIS(inspector, sessionKey, files, receiver);
                case FundType.PFR:
                    return SendFilesPFR(inspector, sessionKey, files, receiver, isLetter: false);

                case FundType.PfrNeformal:
                    return SendFilesPFR(inspector, sessionKey, files, receiver, isLetter: true);

                case FundType.ROSSTAT:
                    // Старый астральный клиент принимал в ОКПО string.Empty, но внутри терял значение и отдавал в ОКПО null.
                    // Именно null в данном случае является правильным.
                    return SendFilesToFsgs(inspector, sessionKey, files, receiver, isLetter: false, okpo: null);

                case FundType.RosstatNeformal:
                    return SendFilesToFsgs(inspector, sessionKey, files, receiver, isLetter: true, okpo: null);

                default:
                    throw new ApplicationException();
            }
        }

        /// <summary>Получение списка Уидов с ответами из органов</summary>
        /// <param name="sessionKey">Ключ сессии</param>
        /// <returns>Список Уидов</returns>
        private FunctionResult<UidWithListReceivers[]> GetNewUidsInner(IMessagesInspector inspector, string sessionKey)
        {
            if (string.IsNullOrEmpty(sessionKey))
            {
                _logger.Error(_loggerTag, "No session key", extraData: new { AstralClientMessage = true });
                return new FunctionResult<UidWithListReceivers[]>(SimpleFunctionResult.InputParameterError);
            }

            try
            {
                using (var client = _astralClientsFactory.CreateWorkflowServiceClient(inspector))
                {
                    var answer = client.GetNewUIDs(new AstralTransferService.RequestHeader { sessionkey = sessionKey }, new GetNewUIDs());
                    return new FunctionResult<UidWithListReceivers[]>(SimpleFunctionResult.True, AstralMapper.MapGetNewUIDsResponseToUidWithListReceivers(answer));
                }
            }
            catch (Exception e)
            {
                _logger.Error(_loggerTag, "AstralClient error", extraData: new { AstralClientMessage = true }, exception: e);
                return new FunctionResult<UidWithListReceivers[]>(SimpleFunctionResult.Exception);
            }
        }

        /// <summary>Получение файлов по Уиду</summary>
        /// <param name="sessionkey">Ключ сессии</param>
        /// <param name="uid">Уид</param>
        /// <returns>Список файлов</returns>
        private FunctionResult<FileObject[]> GetFilesByUidInner(IMessagesInspector inspector, string sessionkey, string uid)
        {
            if (string.IsNullOrEmpty(uid) || string.IsNullOrEmpty(sessionkey))
            {
                _logger.Error(_loggerTag, "No uid or session key", extraData: new { AstralClientMessage = true });
                return new FunctionResult<FileObject[]>(SimpleFunctionResult.InputParameterError);
            }

            try
            {
                using (var client = _astralClientsFactory.CreateWorkflowServiceClient(inspector))
                {
                    var answer = client.GetFilesByUID(new AstralTransferService.RequestHeader { sessionkey = sessionkey }, new GetFilesByUID { UID = uid });
                    return new FunctionResult<FileObject[]>(SimpleFunctionResult.True, AstralMapper.MapGetFilesByUIDResponseToFileObjects(answer));
                }
            }
            catch (Exception e)
            {
                _logger.Error(_loggerTag, "AstralClient error", extraData: new { AstralClientMessage = true }, exception: e);
                return new FunctionResult<FileObject[]>(SimpleFunctionResult.Exception);
            }
        }

        private FunctionResult<FileObject> GetFilesWithUidInner(IMessagesInspector inspector, string uid)
        {
            if (string.IsNullOrEmpty(uid))
            {
                _logger.Error(_loggerTag, "No uid", extraData: new { AstralClientMessage = true });
                return new FunctionResult<FileObject>(SimpleFunctionResult.InputParameterError);
            }

            try
            {
                using (var client = _astralClientsFactory.CreateBankArchiveServiceClient(inspector))
                {
                    var answer = client.GetFilesWithUID(new GetFilesWithUID { UID = uid});
                    return new FunctionResult<FileObject>(SimpleFunctionResult.True, AstralMapper.MapGetFilesWithUidResponse(answer.FilesWithIDs));
                }
            }
            catch (Exception e)
            {
                _logger.Error(_loggerTag, "AstralClient error", extraData: new { AstralClientMessage = true }, exception: e);
                return new FunctionResult<FileObject>(SimpleFunctionResult.Exception);
            }
        }

        private FunctionResult<string> ConfirmFileInner(IMessagesInspector inspector, string sessionkey, string id)
        {
            if (string.IsNullOrEmpty(sessionkey) || string.IsNullOrEmpty(id))
            {
                _logger.Error(_loggerTag, "No uid or session key", extraData: new { AstralClientMessage = true });
                return new FunctionResult<string>(SimpleFunctionResult.InputParameterError);
            }

            using (var client = _astralClientsFactory.CreateWorkflowServiceClient(inspector))
            {
                client.ConfirmFile(new AstralTransferService.RequestHeader { sessionkey = sessionkey }, new ConfirmFile { id = id });
            }

            return new FunctionResult<string>(SimpleFunctionResult.True);
        }

        private FunctionResult<string> ConfirmShippingInner(IMessagesInspector inspector, string sessionkey, string uid, bool agree, string ipAddressOfSender = null)
        {
            if (string.IsNullOrEmpty(sessionkey) || string.IsNullOrEmpty(uid))
            {
                _logger.Error(_loggerTag, "No uid or session key", extraData: new { AstralClientMessage = true });
                return new FunctionResult<string>(SimpleFunctionResult.InputParameterError);
            }

            using (var client = _astralClientsFactory.CreateWorkflowServiceClient(inspector))
            {
                client.ConfirmShipping(new AstralTransferService.RequestHeader { sessionkey = sessionkey }, new ConfirmShipping { Agree = agree, UID = uid, AgreeSpecified = agree, XRealIP = ipAddressOfSender });
            }

            return new FunctionResult<string>(SimpleFunctionResult.True);
        }

        #region Private

        private FunctionResult<string> SendFilesFNS(IMessagesInspector inspector, string sessionKey, List<FileObject> files, string receiver, string xRealIp, string subject, string message, bool isLetter)
        {
            var request = new SendFilesFNS
            {
                ReceiverFNS = receiver,
                Files = AstralMapper.MapFiles(files),
                Subject = subject,
                Message = message,
                XRealIP = xRealIp,
                IsLetter = isLetter,
                IsLetterSpecified = isLetter
            };

            using (var serviceClient = _astralClientsFactory.CreateWorkflowServiceClient(inspector))
            {
                var answer = serviceClient.SendFilesFNS(new AstralTransferService.RequestHeader { sessionkey = sessionKey }, request);
                return new FunctionResult<string>(SimpleFunctionResult.True, answer.UID);
            }
        }

        private FunctionResult<string> SendFilesFSS(IMessagesInspector inspector, string sessionKey, List<FileObject> files)
        {
            var filesFss = new SendFilesFSS
            {
                Files = AstralMapper.MapFiles(files)
            };

            using (var client = _astralClientsFactory.CreateWorkflowServiceClient(inspector))
            {
                var answer = client.SendFilesFSS(new AstralTransferService.RequestHeader { sessionkey = sessionKey }, filesFss);
                return new FunctionResult<string>(SimpleFunctionResult.True, answer.UID);
            }
        }

        private FunctionResult<string> SendFilesPFR(IMessagesInspector inspector, string sessionKey, List<FileObject> files, string receiver, bool isLetter)
        {
            var filesPfr = new SendFilesPFR
            {
                ReceiverPFR = receiver,
                Files = AstralMapper.MapFiles(files),
                IsLetter = isLetter,
                IsLetterSpecified = isLetter
            };

            using (var client = _astralClientsFactory.CreateWorkflowServiceClient(inspector))
            {
                var answer = client.SendFilesPFR(new AstralTransferService.RequestHeader { sessionkey = sessionKey }, filesPfr);
                return new FunctionResult<string>(SimpleFunctionResult.True, answer.UID);
            }
        }

        private FunctionResult<string> SendFilesAIS(IMessagesInspector inspector, string sessionKey, List<FileObject> files, string receiver)
        {
            var filesAis = new SendFilesAIS
            {
                ReceiverAIS = receiver,
                Files = AstralMapper.MapFiles(files)
            };

            using (var client = _astralClientsFactory.CreateWorkflowServiceClient(inspector))
            {
                var answer = client.SendFilesAIS(new AstralTransferService.RequestHeader { sessionkey = sessionKey }, filesAis);
                return new FunctionResult<string>(SimpleFunctionResult.True, answer.UID);
            }
        }

        private FunctionResult<string> SendFilesToFsgs(IMessagesInspector inspector, string sessionKey, List<FileObject> files, string receiver, bool isLetter, string okpo)
        {
            var filesFsgs = new SendFilesStat
            {
                ReceiverStat = receiver,
                Files = AstralMapper.MapFiles(files),
                IsLetter = isLetter,
                OKPO = okpo
            };

            using (var client = _astralClientsFactory.CreateWorkflowServiceClient(inspector))
            {
                var answer = client.SendFilesStat(new AstralTransferService.RequestHeader { sessionkey = sessionKey }, filesFsgs);
                return new FunctionResult<string>(SimpleFunctionResult.True, answer.UID);
            }
        }

        #endregion
    }
}
