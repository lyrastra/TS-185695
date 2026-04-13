using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Documents;
using Moedelo.Common.Enums.Enums.ElectronicReports;
using Moedelo.ErptV2.Dto;
using Moedelo.ErptV2.Dto.ErptDocuments;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.ErptV2.Client.ErptDocuments
{
    public interface IErptDocumentsApiClient : IDI
    {
        Task CopyDocumentsAndRelinkFiles(int fromFirmId, int toFirmId, int toUserId);
        Task<BaseDto> SendSmsCodeAsync(int firmId, int userId);
        Task<BaseDto> GetSessionAsync(int firmId, int userId, string code);
        Task<BaseDto> SendFileAsync(SendFileDto dto);
        Task SendingChangedAsync(SendingChangedDto dto);
        Task<int> EvalReturnedToProcessingStatusAsync(int versionId);
    }
}