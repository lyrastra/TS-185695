using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.KontragentsV2.Dto;

namespace Moedelo.KontragentsV2.Client.Kontragents
{
    public interface IKontragentFavouritesClient : IDI
    {
        /// <summary>
        /// Возвращает топ контрагентов по оборотам в деньгах (за период)
        /// </summary>
        Task<List<KontragentBaseInfoDto>> ByMoneyAsync(int firmId, int userId, int count, DateTime startDate, DateTime endDate);
    }
}