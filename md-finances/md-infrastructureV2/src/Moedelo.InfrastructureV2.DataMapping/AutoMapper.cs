using System;
using System.Collections.Generic;
using System.Linq;

using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.DataMapping;

using Omu.ValueInjecter;

namespace Moedelo.InfrastructureV2.DataMapping;

[InjectAsSingleton(typeof(IAutoMapper))]
public class AutoMapper : IAutoMapper
{
    public TResult Map<TSource, TResult>(TSource fromModel)
    {
        if (fromModel == null)
        {
            return default(TResult);
        }

        var result = Activator.CreateInstance<TResult>();
        result.InjectFrom(fromModel);

        return result;
    }

    public TResult Map<TSource, TResult>(TSource fromModel, Action<TSource, TResult> map)
    {
        var result = Map<TSource, TResult>(fromModel);
        map(fromModel, result);

        return result;
    }

    public void Map<TSource, TResult>(TSource fromModel, TResult toModel)
    {
        if (toModel == null)
        {
            throw new ArgumentNullException(nameof(toModel), "Can not map into null model");
        }

        if (fromModel == null)
        {
            return;
        }

        toModel.InjectFrom(fromModel);
    }

    public void Map<TSource, TResult>(TSource fromModel, TResult toModel, Action<TSource, TResult> map)
    {
        Map(fromModel, toModel);
        map(fromModel, toModel);
    }

    public List<TResult> MapList<TSource, TResult>(IReadOnlyCollection<TSource> listOfSources)
    {
        return listOfSources?.Select(Map<TSource, TResult>).ToList() ?? [];
    }

    public List<TResult> MapList<TSource, TResult>(IReadOnlyCollection<TSource> listOfSources, Action<TSource, TResult> map)
    {
        return listOfSources?.Select(s => Map(s, map)).ToList() ?? [];
    }
        
    public TResult[] MapArray<TSource, TResult>(IReadOnlyCollection<TSource> listOfSources)
    {
        return listOfSources?.Select(Map<TSource, TResult>).ToArray() ?? [];
    }

    public TResult[] MapArray<TSource, TResult>(IReadOnlyCollection<TSource> listOfSources, Action<TSource, TResult> map)
    {
        return listOfSources?.Select(s => Map(s, map)).ToArray() ?? [];
    }
}