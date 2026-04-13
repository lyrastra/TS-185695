using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.BizToAccMoneySource;

namespace Moedelo.AccountingV2.Client.BizToAccMoneySource
{
    public interface IBizToAccMoneySourceApiClient
    {
        Task<IReadOnlyCollection<BizPaymentDocumentDto>> GetMoneySourceDataAsync(
            int firmId, int userId, DateTime onDate);
    }
}
