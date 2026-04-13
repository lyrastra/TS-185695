using System.Threading.Tasks;
using System.Web.Http;
using Moedelo.CommonV2.Auth.WebApi;
using Moedelo.Finances.Api.Mappers.Money;
using Moedelo.Finances.Domain.Interfaces.Business.Money;
using Moedelo.Finances.Dto.Money.Duplicates;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Business.Services.Money.Operations.Duplicates;
using Moedelo.Common.Enums.Enums.Access;
using Moedelo.Finances.Domain.Interfaces.Business.Money.Duplicates;

namespace Moedelo.Finances.Api.Controllers.Money
{
    [RoutePrefix("Money/Duplicates")]
    [WebApiRejectUnauthorizedRequest]
    public class DuplicatesController : ApiController
    {
        private readonly IUserContext userContext;
        private readonly IMoneyOperationDuplicatesReader reader;
        private readonly IDuplicateDetector detector;
        private readonly IDuplicatesMapper mapper;

        public DuplicatesController(
            IUserContext userContext,
            IMoneyOperationDuplicatesReader reader,
            DuplicateDetector detector,
            IDuplicatesMapper mapper)
        {
            this.userContext = userContext;
            this.reader = reader;
            this.detector = detector;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("GetRoboAndSapeIncomingOperationId")]
        public async Task<int?> GetRoboAndSapeIncomingOperationIdAsync(DuplicateRoboAndSapeOperationRequestDto dto)
        {
            var request = mapper.Map(dto);
            return await reader.GetRoboAndSapeIncomingOperationIdAsync(userContext, request).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("GetRoboAndSapeOutgoingOperationId")]
        public async Task<int?> GetRoboAndSapeOutgoingOperationIdAsync(DuplicateRoboAndSapeOperationRequestDto dto)
        {
            var request = mapper.Map(dto);
            return await reader.GetRoboAndSapeOutgoingOperationIdAsync(userContext, request).ConfigureAwait(false);
        }


        [HttpPost]
        [Route("GetYandexIncomingOperationId")]
        public async Task<int?> GetYandexIncomingOperationIdAsync(DuplicateYandexOperationRequestDto dto)
        {
            var request = mapper.Map(dto);
            return await reader.GetYandexIncomingOperationIdAsync(userContext, request).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("GetYandexOutgoingOperationId")]
        public async Task<int?> GetYandexOutgoingOperationIdAsync(DuplicateYandexOperationRequestDto dto)
        {
            var request = mapper.Map(dto);
            return await reader.GetYandexOutgoingOperationIdAsync(userContext, request).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("GetYandexMovementOperationId")]
        public async Task<int?> GetYandexMovementOperationIdAsync(DuplicateMovementYandexOperationRequestDto dto)
        {
            var request = mapper.Map(dto);
            return await reader.GetYandexMovementOperationIdAsync(userContext, request).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("GetIncomingOperationIdExt")]
        public async Task<DuplicateResultDto> GetIncomingOperationIdExtAsync(DuplicateIncomingOperationRequestDto dto)
        {
            var request = mapper.Map(dto);
            var result = await reader.GetIncomingOperationIdExtAsync(userContext, request).ConfigureAwait(false);
            return mapper.Map(result);
        }

        [HttpPost]
        [Route("GetOutgoingOperationIdExt")]
        public async Task<DuplicateResultDto> GetOutgoingOperationIdExtAsync(DuplicateOutgoingOperationRequestDto dto)
        {
            var request = mapper.Map(dto);
            var result = await reader.GetOutgoingOperationIdExtAsync(userContext, request).ConfigureAwait(false);
            return mapper.Map(result);
        }

        [HttpPost]
        [Route("GetBankFeeOutgoingOperationIdExt")]
        public async Task<DuplicateResultDto> GetBankFeeOutgoingOperationIdExtAsync(DuplicateBankFeeOutgoingOperationRequestDto dto)
        {
            var request = mapper.Map(dto);
            var result = await reader.GetOutgoingOperationIdExtAsync(userContext, request).ConfigureAwait(false);
            return mapper.Map(result);
        }

        [HttpPost]
        [Route("Detect")]
        public async Task<DuplicateDetectionResultDto[]> DetectAsync(DuplicateDetectionRequestDto dto)
        {
            var isAccounting = await userContext.HasAllRuleAsync(AccessRule.UsnAccountantTariff).ConfigureAwait(false);
            var request = DuplicatesMapper.Map(userContext.FirmId, isAccounting, dto);
            var results = await detector.DetectAsync(request).ConfigureAwait(false);
            return DuplicatesMapper.Map(results);
        }
    }
}
