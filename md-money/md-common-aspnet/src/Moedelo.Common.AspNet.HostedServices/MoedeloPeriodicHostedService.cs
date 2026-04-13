using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Audit.Abstractions.Models;
using Moedelo.Infrastructure.AspNetCore.HostedServices;

namespace Moedelo.Common.AspNet.HostedServices;

/// <summary>
/// Hosted сервис, который последовательно вызывает <see cref="DoExecuteTaskAsync"/> через паузу <see cref="PeriodicHostedService.Interval"/>
/// Обеспечивает запись данных в систему auditTrail
/// Как использовать:
/// - создать класс наследник <see cref="MoedeloPeriodicHostedService"/>
/// - реализовать метод <see cref="DoExecuteTaskAsync"/> в классе наследнике
/// - для изменения периода вызова метода <see cref="DoExecuteTaskAsync"/> следует переопределить свойство <see cref="PeriodicHostedService.Interval"/>
/// </summary>
public abstract class MoedeloPeriodicHostedService : PeriodicHostedService
{
    private readonly IAuditTracer auditTracer;
    private readonly string auditSpanName;

    protected MoedeloPeriodicHostedService(
        IAuditTracer auditTracer,
        ILogger logger) : base(logger)
    {
        this.auditTracer = auditTracer;
        this.auditSpanName = $"{GetType().Name}.{nameof(DoExecuteTaskAsync)}";
    }

    /// <summary>
    /// Метод, реализующий бизнес-логику периодической активности
    /// Будет вызываться периодически через паузу, определяемую <see cref="PeriodicHostedService.Interval"/>
    /// </summary>
    /// <param name="cancellationToken">токен отмены операции</param>
    protected abstract Task DoExecuteTaskAsync(CancellationToken cancellationToken);

    protected sealed override async Task ExecuteTaskAsync(CancellationToken cancellationToken)
    {
        using var scope = auditTracer
            .BuildSpan(AuditSpanType.InternalCode, auditSpanName)
            .Start();

        try
        {
            await DoExecuteTaskAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            scope.Span.SetError(exception);
            throw;
        }
    }
}