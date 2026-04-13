using Moedelo.Common.ExecutionContext.Abstractions.Models;

namespace Moedelo.Common.ExecutionContext.Abstractions.Interfaces;

public interface IExecutionInfoContextInitializer
{
    ExecutionInfoContext Initialize(string token);
    ExecutionInfoContext Initialize(string token, ExecutionInfoContextTokenDecodingOptions options);
}