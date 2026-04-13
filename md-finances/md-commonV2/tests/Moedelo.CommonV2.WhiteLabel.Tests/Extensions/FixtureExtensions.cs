using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;

namespace Moedelo.CommonV2.WhiteLabel.Tests.Extensions;

public static class FixtureExtensions
{
    public static IEnumerable<T> GenerateEnumValuesUseExclusions<T>(
        this Fixture fixture,
        int count,
        params T[] exclusions)
        where T : Enum
    {
        var enumType = typeof(T);
        if (enumType == null || !enumType.IsEnum)
            throw new ObjectCreationException("This is not enum or null");
        if (exclusions == null)
            throw new ObjectCreationException("You should set the exclusions");
        var values = ((T[])Enum.GetValues(typeof(T))).Where(v => !exclusions.Contains(v)).ToList();

        while (count-- > 0)
        {
            yield return values[fixture.Create<int>() % values.Count];
        }
    }
}