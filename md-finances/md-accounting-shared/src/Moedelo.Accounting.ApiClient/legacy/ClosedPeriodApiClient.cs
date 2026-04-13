using Microsoft.Extensions.Logging;
using Moedelo.Accounting.ApiClient.Abstractions.legacy;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using System;
using System.Threading.Tasks;
using Moedelo.Accounting.Enums.ClosedPeriods;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.ClosedPeriods;
using System.Collections.Generic;

namespace Moedelo.Accounting.ApiClient.legacy
{
    [InjectAsSingleton(typeof(IClosedPeriodApiClient))]
    internal sealed class ClosedPeriodApiClient : BaseLegacyApiClient, IClosedPeriodApiClient
    {
        public ClosedPeriodApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<ClosedPeriodApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("AccountingApiEndpoint"),
                logger)
        {
        }

        public async Task<DateTime> GetLastClosedDateAsync(FirmId firmId, UserId userId)
        {
            var uri = $"/ClosedPeriodApi/GetLastClosedDate?firmId={firmId}&userId={userId}";
            var response = await GetAsync<DataResponseWrapper<string>>(uri).ConfigureAwait(false);

            return DateTime.TryParse(response.Data, out var parsedDate)
                ? parsedDate
                : DateTime.MinValue;
        }

        public async Task<DateTime> GetFirstOpenedDateAsync(FirmId firmId, UserId userId)
        {
            var uri = $"/ClosedPeriodApi/GetFirstOpenedDate?firmId={firmId}&userId={userId}";
            var response = await GetAsync<DataResponseWrapper<DateTime>>(uri).ConfigureAwait(false);

            return response.Data;
        }

        public async Task<ClosedPeriodDto> GetLastClosedPeriodAsync(FirmId firmId, UserId userId)
        {
            var uri = $"/ClosedPeriods/Last?firmId={firmId}&userId={userId}";
            var response = await GetAsync<DataResponseWrapper<ClosedPeriodDto>>(uri).ConfigureAwait(false);

            return response.Data;
        }

        public Task<IReadOnlyDictionary<int, DateTime>> GetLastClosedDateByFirmIdsAsync(
            IReadOnlyCollection<int> firmIds)
        {
            var uri = $"/ClosedPeriodsSummary/LastDateByFirmIds";
            return PostAsync<IReadOnlyCollection<int>, IReadOnlyDictionary<int, DateTime>>(uri, firmIds);
        }

        public Task<CheckPerionValidationDto> CheckPeriodAsync(FirmId firmId, UserId userId, DateTime startDate, DateTime endDate, int timeoutMs = 30_000)
        {
            if (timeoutMs <= 0 || timeoutMs >= 120_000)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(timeoutMs),
                    "Некорректный таймаут. Значение должно быть в диапазоне (0...120_000)"
                );
            }
            
            var uri = $"/ClosedPeriodApi/CheckPeriod?firmId={firmId}&userId={userId}";
            return PostAsync<object, CheckPerionValidationDto>(uri, new
                {
                    StartDate = startDate,
                    EndDate = endDate
                },
                setting: new HttpQuerySetting(TimeSpan.FromMilliseconds(timeoutMs))
            );
        }

        public Task CloseMonthAsync(
            FirmId firmId,
            UserId userId,
            DateTime upToDate,
            CloseType closeType,
            int timeoutMs = 30_000)
        {
            if (timeoutMs <= 0 || timeoutMs >= 300_000)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(timeoutMs),
                    "Некорректный таймаут. Значение должно быть в диапазоне (0...300_000)"
                );
            }
            
            var uri = $"/ClosedPeriodApi/CloseMonth?firmId={firmId}&userId={userId}&date={upToDate:yyyy-MM-dd}&closeType={(int)closeType}";
            return PostAsync(uri, setting: new HttpQuerySetting(TimeSpan.FromMilliseconds(timeoutMs)));
        }

        public Task ClosePeriodByCalendarIdAsync(
            FirmId firmId,
            UserId userId,
            int calendarId,
            CloseType closeType,
            int timeoutMs = 30_000)
        {
            if (timeoutMs <= 0 || timeoutMs >= 300_000)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(timeoutMs),
                    "Некорректный таймаут. Значение должно быть в диапазоне (0...300_000)"
                );
            }
            
            var uri = $"/ClosedPeriodApi/ClosePeriodByCalendarId?firmId={firmId}&userId={userId}&calendarId={calendarId}&closeType={(int)closeType}";
            return PostAsync(uri, setting: new HttpQuerySetting(TimeSpan.FromMilliseconds(timeoutMs)));
        }
    }
}