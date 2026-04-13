using System.Collections.Generic;
using Moedelo.Common.AccessRules.Abstractions;

namespace Moedelo.Billing.Abstractions.Legacy.Dto.Tariffs;

public class TariffFilterDto
{
    /// <summary>
    /// Включать в результат только тарифы с указанными платформами
    /// </summary>
    public IReadOnlyCollection<string> IncludeProductPlatforms { get; set; } = null;

    /// <summary>
    /// Включать в результат только тарифы, выдающие пользователю все из указанных прав
    /// ВНИМАНИЕ: актуально только для легаси-тарифов. Не имеет смысла для тарифов нового биллинга
    /// </summary>
    public IReadOnlyCollection<AccessRule> WithAllPermission { get; set; } = null;

    /// <summary>
    /// Включать в результат только тарифы, выдающие пользователю любое из указанных прав
    /// ВНИМАНИЕ: актуально только для легаси-тарифов. Не имеет смысла для тарифов нового биллинга
    /// </summary>
    public IReadOnlyCollection<AccessRule> WithAnyPermission { get; set; } = null;

    /// <summary>
    /// Исключить из результата тарифы, выдающие пользователю любое из указанных прав
    /// ВНИМАНИЕ: актуально только для легаси-тарифов. Не имеет смысла для тарифов нового биллинга
    /// </summary>
    public IReadOnlyCollection<AccessRule> WithoutAllPermission { get; set; } = null;
}