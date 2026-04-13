using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Moedelo.Common.Enums.Enums.Access;
using Moedelo.Common.Enums.Enums.WhiteLabels;

[assembly: InternalsVisibleTo("Moedelo.CommonV2.WhiteLabel.Tests")]

namespace Moedelo.CommonV2.WhiteLabel.Services
{
    internal static class WhiteLabelMapping
    {
        internal static readonly IReadOnlyDictionary<WhiteLabelType, AccessRule[]> WhiteLabelTypePermissions = new Dictionary<WhiteLabelType, AccessRule[]>
        {
            {WhiteLabelType.AkbarsBank, new[] { AccessRule.AkbarsBankTariff } },
            {WhiteLabelType.DeloBank, new[] { AccessRule.SkbBankWlTariff } },
            {WhiteLabelType.ProstoBank, new[] { AccessRule.ProstoBankTariff } },
            {WhiteLabelType.Rncb, new[] { AccessRule.RnkbTariff } },
            {WhiteLabelType.Sberbank, new[] { AccessRule.SberbankWLSubscriptionAny, AccessRule.SberbankTariff, AccessRule.SberbankWLOperator } },
            {WhiteLabelType.Gis, new[] { AccessRule.GisTariff } },
            {WhiteLabelType.Sovcombank, new[] { AccessRule.SovcombankWlTariff } },
            {WhiteLabelType.WbBank, new[] { AccessRule.WbBankWlTariff } },
        };

        internal static readonly IReadOnlyDictionary<AccessRule, WhiteLabelType> PermissionToWhiteLabelType =
            WhiteLabelTypePermissions
                .SelectMany(pair => pair.Value
                    .Select(permission => new KeyValuePair<AccessRule, WhiteLabelType>(permission, pair.Key)))
                .ToDictionary(pair => pair.Key, pair => pair.Value);

        internal static readonly AccessRule[] PermissionWithWhiteLabelAccess = PermissionToWhiteLabelType.Keys.ToArray();

    }
}
