using System;
using System.Threading.Tasks;
using Moedelo.ErptV2.Dto.EdsRequestTasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.ErptV2.Client.EdsRequestTasks
{
    [Obsolete("Все api методы на вызываемой стороне удалены и будут приводить к 404")]
    public interface IEdsRequestTasksClient : IDI
    {
        Task<EdsRequestTaskResponseDto> GetListAsync(EdsRequestTaskRequestDto request);
        Task<EdsRequestTaskDto> GetAsync(int id);
        Task SetAsync(EdsRequestTaskSetDto request);
        Task SetAllOutdatedAsync(int firmId);
        Task ActualizeAsync();
    }
}