using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Moedelo.InfrastructureV2.Domain.Interfaces.DataMapping;

public interface IAutoMapper
{
    TR Map<T, TR>(T fromModel);

    TR Map<T, TR>(T fromModel, Action<T, TR> map);

    void Map<T, TR>(T fromModel, TR toModel);

    void Map<T, TR>(T fromModel, TR toModel, Action<T, TR> map);

    List<TR> MapList<T, TR>(IReadOnlyCollection<T> listOfSources);

    List<TR> MapList<T, TR>(IReadOnlyCollection<T> listOfSources, Action<T, TR> map);

    TR[] MapArray<T, TR>(IReadOnlyCollection<T> listOfSources);

    TR[] MapArray<T, TR>(IReadOnlyCollection<T> listOfSources, Action<T, TR> map);
}