using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.AspNet.HostedServices.Extensions;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Consul.Abstractions;
using Moedelo.Infrastructure.AspNetCore.HostedServices;
using System;

namespace Moedelo.Common.AspNet.HostedServices;

/// <summary>
/// Hosted сервис, который через паузу <see cref="PeriodicHostedService.Interval"/>
/// пытается захватить эксклюзивный доступ к <see cref="LeadershipLockId"/>.
/// Если ресурс захвачен, то вызывается метод <see cref="ExecuteTaskExclusivelyAsync"/>, иначе ничего не делается.
/// Обеспечивает запись данных в систему auditTrail.
/// Как использовать:
/// - создать класс наследник <see cref="MoedeloExclusivePeriodicHostedService"/>
/// - определить свойство <see cref="LeadershipLockId"/> - должно быть уникальным в контексте приложения
/// - реализовать метод <see cref="ExecuteTaskExclusivelyAsync"/> в классе наследнике
/// - для изменения периода следует переопределить свойство <see cref="PeriodicHostedService.Interval"/>
/// </summary>
public abstract class MoedeloExclusivePeriodicHostedService : MoedeloPeriodicHostedService
{
    private readonly IMoedeloServiceLeadershipService leadershipService;
    // ReSharper disable once RedundantDefaultMemberInitializer
    private bool? isMaster = null;
    private readonly string typeName; 

    protected MoedeloExclusivePeriodicHostedService(
        IMoedeloServiceLeadershipService leadershipService,
        IAuditTracer auditTracer,
        ILogger logger) : base(auditTracer, logger)
    {
        typeName = GetType().Name;
        this.leadershipService = leadershipService;
    }
    
    private bool IsMaster => isMaster ?? false;

    protected override void OnBeforeStart()
    {
        // Валидация LeadershipLockId на соответствие ограничениям Consul
        LeadershipLockId.ValidateLeadershipLockId();

        base.OnBeforeStart();
    }

    /// <summary>
    /// Метод, реализующий бизнес-логику периодической активности
    /// Будет вызываться периодически через паузу, определяемую <see cref="PeriodicHostedService.Interval"/>
    /// </summary>
    /// <param name="cancellationToken">токен отмены операции</param>
    protected abstract Task ExecuteTaskExclusivelyAsync(CancellationToken cancellationToken);
    
    protected abstract string LeadershipLockId { get; }

    protected sealed override async Task DoExecuteTaskAsync(CancellationToken cancellationToken)
    {
        var prevValue = isMaster;

        try
        {
            logger.LogLeadershipAcquiring(typeName, LeadershipLockId);
            
            isMaster = await leadershipService.AcquireLeadershipAsync(LeadershipLockId, cancellationToken);

            logger.LogLeadershipAcquiringResult(typeName, isMaster, leadershipService.ConsulSessionId);

            if (prevValue != isMaster)
            {
                if (IsMaster)
                {
                    logger.LogBecameMaster(typeName, leadershipService.ConsulSessionId);
                }
                else
                {
                    logger.LogNotMaster(typeName, leadershipService.ConsulSessionId);
                }
            }

            if (IsMaster)
            {
                await ExecuteTaskExclusivelyAsync(cancellationToken);
            }
        }
        catch (Exception ex)
        {
            logger.LogLeadershipAcquiringError(ex, typeName, LeadershipLockId);
            // состояние не определено
            isMaster = null;
            throw;
        }
    }

    /// <summary>
    /// Освобождает ресурсы и освобождает лидерство при остановке сервиса
    /// </summary>
    protected override async Task OnBeforeStopAsync(CancellationToken cancellationToken)
    {
        // Освобождение лидерства при завершении работы
        if (isMaster == true)
        {
            logger.LogLeadershipReleasing(typeName, LeadershipLockId);

            try
            {
                await leadershipService
                    .ReleaseLeadershipAsync(LeadershipLockId, cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogLeadershipReleasingError(ex, typeName, LeadershipLockId);
            }
        }
        
        await base.OnBeforeStopAsync(cancellationToken);
    }
}
