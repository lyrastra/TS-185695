using System;
using Moedelo.ErptV2.Dto.Flc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.ErptV2.Client.Flc
{
    public interface IFlcApiClient
    {
        [Obsolete("Use IFlcNetCoreApiClient::CheckFilesAsync instead")]
        Task<List<FlcProtocolDto>> VerifyFormatAsync(FlcDataDto flcDataDto, int firmId = 0);

        [Obsolete("Use IFlcNetCoreApiClient::CheckFilesAsync instead")]
        Task<Dictionary<string, List<string>>> VerifyAsync(FlcDataDto flcDataDto, int firmId = 0);

        Task<FlcResultDto> VerifyContentAsync(FileDataDto fileDataDto, int firmId, int userId, bool isManualSending = false);
    }
}
