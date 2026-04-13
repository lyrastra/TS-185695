using Microsoft.Extensions.Logging;
using Moedelo.Common.Consul.Internals.Extensions;
using Moedelo.Infrastructure.Consul.Exceptions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Common.Consul.Internals;

[InjectAsSingleton(typeof(IMoedeloConsulSessionMonitoring))]
internal sealed class MoedeloConsulSessionMonitoring : IMoedeloConsulSessionMonitoring
{
    private readonly ILogger logger;

    public MoedeloConsulSessionMonitoring(ILogger<MoedeloConsulSessionMonitoring> logger)
    {
        this.logger = logger;
    }

    public void OnSessionCreationStarted(string sessionName)
    {
        logger.LogSessionCreationStarted(sessionName);
    }

    public void OnSessionCreated(string sessionId, string sessionName)
    {
        logger.LogSessionCreated(sessionId, sessionName);
    }

    public void OnSessionCreationFailed(string sessionName, Exception error)
    {
        if (error is ConsulSessionCreationException creationEx)
        {
            logger.LogSessionCreationFailed(error, creationEx.SessionName);
        }
        else
        {
            logger.LogSessionCreationFailed(error, sessionName);
        }
    }

    public void OnSessionValidationStarted(string sessionId)
    {
        logger.LogSessionValidationStarted(sessionId);
    }

    public void OnSessionValidated(string sessionId)
    {
        logger.LogSessionValidated(sessionId);
    }

    public void OnSessionValidationFailed(string sessionId, Exception error)
    {
        if (error is ConsulSessionValidationException validationEx)
        {
            logger.LogSessionValidationFailed(error, validationEx.SessionId);
        }
        else
        {
            logger.LogSessionValidationFailed(error, sessionId);
        }
    }

    public void OnSessionRenewalStarted(string sessionId)
    {
        logger.LogSessionRenewalStarted(sessionId);
    }

    public void OnSessionRenewed(string sessionId)
    {
        logger.LogSessionRenewed(sessionId);
    }

    public void OnSessionRenewalFailed(string sessionId, Exception error)
    {
        logger.LogSessionRenewalFailed(error, sessionId);
    }

    public void OnKeyAcquisitionStarted(string sessionId, string keyPath)
    {
        logger.LogKeyAcquisitionStarted(sessionId, keyPath);
    }

    public void OnKeyAcquisitionCompleted(string sessionId, string keyPath, bool success)
    {
        if (success)
        {
            logger.LogKeyAcquisitionCompleted(sessionId, keyPath);
        }
        else
        {
            logger.LogKeyAcquisitionFailed(sessionId, keyPath);
        }
    }

    public void OnKeyAcquisitionFailed(string sessionId, string keyPath, Exception error)
    {
        if (error is ConsulKeyAcquisitionException acquisitionEx)
        {
            logger.LogKeyAcquisitionError(error, acquisitionEx.SessionId, acquisitionEx.KeyPath);
        }
        else
        {
            logger.LogKeyAcquisitionError(error, sessionId, keyPath);
        }
    }

    public void OnKeyReleaseStarted(string sessionId, string keyPath)
    {
        logger.LogKeyReleaseStarted(sessionId, keyPath);
    }

    public void OnKeyReleaseCompleted(string sessionId, string keyPath)
    {
        logger.LogKeyReleaseCompleted(sessionId, keyPath);
    }

    public void OnKeyReleaseFailed(string sessionId, string keyPath, Exception error)
    {
        logger.LogKeyReleaseFailed(error, sessionId, keyPath);
    }

    public void OnSessionDisposed(string sessionId)
    {
        logger.LogSessionDisposed(sessionId);
    }
} 