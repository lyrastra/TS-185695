using Moedelo.Common.ExecutionContext.Abstractions.Models;
using System;

namespace Moedelo.Common.ExecutionContext.Abstractions.Interfaces;

public interface IExecutionInfoContextAccessor
{
    ExecutionInfoContext ExecutionInfoContext { get; }

    string ExecutionInfoContextToken { get; }

    IDisposable SetContext(string token, ExecutionInfoContext context);
}