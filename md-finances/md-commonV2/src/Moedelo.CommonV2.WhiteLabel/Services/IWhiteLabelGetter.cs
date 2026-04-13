using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Access;
using Moedelo.Common.Enums.Enums.WhiteLabels;

namespace Moedelo.CommonV2.WhiteLabel.Services;

public interface IWhiteLabelGetter
{
    bool IsWhiteLabel(IReadOnlyCollection<AccessRule> rules);
    Task<WhiteLabelType> GetAsync(int firmId, int userId);
    WhiteLabelType GetByRules(IEnumerable<AccessRule> rules);
}