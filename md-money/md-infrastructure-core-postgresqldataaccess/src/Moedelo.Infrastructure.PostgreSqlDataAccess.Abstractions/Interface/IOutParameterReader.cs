﻿namespace Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions.Interface
{
    public interface IOutParameterReader
    {
        T Read<T>(string paramName);
    }
}