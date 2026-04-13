using Moedelo.AstralV3.Client.AstralFlcServiceReference;
using Moedelo.AstralV3.Client.AstralInteractionsLogger.MessagesInspector;
using Moedelo.AstralV3.Client.Helpers;
using Moedelo.AstralV3.Client.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.AstralV3.Client.AstralClient
{
    /// <summary>
    /// Часть класса AstralClient, ответственная за ФЛК
    /// </summary>
    public partial class AstralClient
    {
        private FunctionResult<FlcResult> CheckFilesInner(IMessagesInspector inspector, List<FileObject> filesForChecking)
        {
            if (!filesForChecking.Any())
            {
                _logger.Error(_loggerTag, "No files to check", extraData: new { AstralClientMessage = true });
                return new FunctionResult<FlcResult>(SimpleFunctionResult.InputParameterError);
            }

            try
            {
                var files = filesForChecking.Select(x => new File { content = x.Content, filename = x.Name }).ToArray();
                var request = new CheckFiles
                {
                    Files = files
                };

                using (var client = _astralClientsFactory.CreateFlkServiceClient(inspector))
                {
                    var result = client.CheckFiles(request);
                    return new FunctionResult<FlcResult>(SimpleFunctionResult.True, AstralMapper.MapCheckFilesResponseToFlcResult(result));
                }
            }
            catch (Exception e)
            {
                _logger.Error(_loggerTag, "AstralClient error", e, extraData: new { AstralClientMessage = true });
                return new FunctionResult<FlcResult>(SimpleFunctionResult.Exception);
            }
        }
    }
}
