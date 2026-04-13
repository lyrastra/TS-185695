using System;
using Moedelo.Common.ExecutionContext.Abstractions.Models;

namespace Moedelo.Common.ExecutionContext.Unidentified;

internal record ExecutionContextData(
    string Token,
    ExecutionInfoContext Context,
    DateTime ValidUntil);
