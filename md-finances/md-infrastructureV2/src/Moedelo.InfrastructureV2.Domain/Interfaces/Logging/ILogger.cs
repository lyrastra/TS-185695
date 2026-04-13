using System;
using System.Runtime.CompilerServices;
using Moedelo.InfrastructureV2.Domain.Interfaces.Context;
using Moedelo.InfrastructureV2.Domain.Interfaces.Http;

namespace Moedelo.InfrastructureV2.Domain.Interfaces.Logging;

public interface ILogger
{
    void Trace(
        string tag, 
        string message, 
        IAuditContext context = null, 
        object extraData = null,
        IHttpEnviroment environment = null, 
        [CallerMemberName] string callerMemberName = "",
        [CallerFilePath] string callerFilePath = "",
        [CallerLineNumber] int callerLineNumber = 0);

    void Debug(
        string tag, 
        string message, 
        IAuditContext context = null, 
        object extraData = null, 
        IHttpEnviroment environment = null, 
        [CallerMemberName] string callerMemberName = "",
        [CallerFilePath] string callerFilePath = "",
        [CallerLineNumber] int callerLineNumber = 0);

    void Info(
        string tag, 
        string message, 
        IAuditContext context = null, 
        object extraData = null, 
        IHttpEnviroment environment = null, 
        [CallerMemberName] string callerMemberName = "",
        [CallerFilePath] string callerFilePath = "",
        [CallerLineNumber] int callerLineNumber = 0);

    void Warning(
        string tag, 
        string message, 
        Exception exception = null, 
        IAuditContext context = null, 
        object extraData = null, 
        IHttpEnviroment environment = null, 
        [CallerMemberName] string callerMemberName = "",
        [CallerFilePath] string callerFilePath = "",
        [CallerLineNumber] int callerLineNumber = 0);

    void Error(
        string tag, 
        string message, 
        Exception exception = null, 
        IAuditContext context = null, 
        object extraData = null, 
        IHttpEnviroment environment = null, 
        [CallerMemberName] string callerMemberName = "",
        [CallerFilePath] string callerFilePath = "",
        [CallerLineNumber] int callerLineNumber = 0);

    void Fatal(
        string tag, 
        string message, 
        Exception exception = null, 
        IAuditContext context = null, 
        object extraData = null, 
        IHttpEnviroment environment = null, 
        [CallerMemberName] string callerMemberName = "",
        [CallerFilePath] string callerFilePath = "",
        [CallerLineNumber] int callerLineNumber = 0);

    void TraceIfError(
        string tag, 
        string message, 
        IAuditContext context = null, 
        object extraData = null, 
        IHttpEnviroment environment = null, 
        [CallerMemberName] string callerMemberName = "",
        [CallerFilePath] string callerFilePath = "",
        [CallerLineNumber] int callerLineNumber = 0);

    void DebugIfError(
        string tag, 
        string message, 
        IAuditContext context = null, 
        object extraData = null, 
        IHttpEnviroment environment = null, 
        [CallerMemberName] string callerMemberName = "",
        [CallerFilePath] string callerFilePath = "",
        [CallerLineNumber] int callerLineNumber = 0);

    void AddLogEventExtender(ILogEventExtender extender);
}