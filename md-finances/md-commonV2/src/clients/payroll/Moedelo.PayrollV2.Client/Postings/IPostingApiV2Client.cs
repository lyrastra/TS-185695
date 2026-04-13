using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.PayrollV2.Dto.Postings;

namespace Moedelo.PayrollV2.Client.Postings
{
    public interface IPostingApiV2Client : IDI
    {
        /// <summary>
        /// Получить все проводки по зарплате за период для УС
        /// </summary>
        Task<SalaryPostingsModelV2Dto> GetPostingsAsync(int firmId, int userId, DateTime startDate, DateTime endDate);
        
        /// <summary>
        /// Провести зарплатные проводки в бух.учете за период для УС
        /// </summary>
        Task<List<SalaryPostingDto>> ProvideAsync(int firmId, int userId, DateTime startDate, DateTime endDate);

        /// <summary>
        /// Получить все проводки по зарплате за период для биза
        /// </summary>
        Task<List<SalaryPostingForBizDto>> GetPostingsForBizAsync(int firmId, int userId, DateTime startDate,
            DateTime endDate);
    }
}