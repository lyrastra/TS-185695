namespace Moedelo.Common.Audit.Abstractions.Interfaces;

/// <summary>
/// Интерфейс для записи спанов в auditTrail напрямую
/// </summary>
public interface IAuditTrailExplicitLogger
{
    /// <summary>
    /// Записать span в систему auditTrail
    /// ВНИМАНИЕ: корректность данных внутри span целиком и полностью лежит на вызывающей стороне
    /// </summary>
    /// <param name="span">спан auditTrail</param>
    void WriteSpan(IAuditSpanData span);
}
