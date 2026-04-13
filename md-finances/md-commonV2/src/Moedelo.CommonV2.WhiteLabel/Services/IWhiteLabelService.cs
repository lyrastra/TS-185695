using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Access;

namespace Moedelo.CommonV2.WhiteLabel.Services;

public interface IWhiteLabelService
{
    string GetNameByHost(string host);

    [Obsolete("Use WhiteLabelGetter.IsWhiteLabel instead")]
    bool IsTariffWl(ISet<AccessRule> accessRules);
}