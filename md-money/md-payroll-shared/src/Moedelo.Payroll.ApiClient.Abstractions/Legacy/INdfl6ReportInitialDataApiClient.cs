using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Ndfl6ReportInitialData;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy
{
    [Obsolete("Используется в отчетах до 2023 года")]
    public interface INdfl6ReportInitialDataApiClient
    {
        Task<Ndfl6ReportInitialDataDto> GetInitialDataAsync(int firmId, int userId, int year, int periodNumber,
            int ndflSettingId);

        Task<List<int>> GetNdflSettingListAsync(int firmId, int userId, int year, int periodNumber);
    }
}