using System;

namespace Moedelo.InfrastructureV2.Domain.Helpers;

internal readonly record struct DataColumnType(Type Type, bool AllowDbNull);
