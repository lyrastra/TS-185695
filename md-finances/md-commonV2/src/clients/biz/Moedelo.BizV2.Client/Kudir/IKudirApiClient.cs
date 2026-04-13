using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.BizV2.Dto.Kudir;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.BizV2.Client.Kudir
{
    public interface IKudirApiClient : IDI
    {
        Task<KudirDownloadResult> Download(int firmId, int userId, KudirDownloadDto dto);

        Task<List<KudirBudgetaryPaymentDto>> GetBudgetaryPayments(int firmId, int userId, int year);

        Task<List<SickKudirDto>> GetKudirSicklistPayments(int firmId, int userId, int year);

        Task<QuarterlyTaxPostingsDto> GetQuarterlyTaxPostingsAsync(int firmId, int userId, int year, DateTime? onDate = null);

        Task<QuarterlyTaxPostingsDto> GetQuarterlyTaxPostingsWithPatentAsync(int firmId, int userId, int year, DateTime? onDate = null, CancellationToken ct = default);

        /// <summary>
        /// Суммы взносов в фонд за себя (за ИП)
        /// </summary>
        /// <param name="year">Отчетный год</param>
        Task<FundPaymentSumForUsnIpDto> GetFundPaymentSumForUsnIpAsync(int firmId, int userId, int year, CancellationToken cancellationToken);
    }
}
