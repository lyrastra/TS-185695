using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Payroll.ApiClient.Abstractions.Ndfl2IncomeStatement.Dto;

namespace Moedelo.Payroll.ApiClient.Abstractions.Ndfl2IncomeStatement
{
    public interface INdfl2IncomeStatementApiClient
    {
        Task<IReadOnlyCollection<Ndfl2IncomeStatementDataDto>> GetDataAsync(FirmId firmId, UserId userId, 
            Ndfl2IncomeStatementRequestDto request);
    }
}