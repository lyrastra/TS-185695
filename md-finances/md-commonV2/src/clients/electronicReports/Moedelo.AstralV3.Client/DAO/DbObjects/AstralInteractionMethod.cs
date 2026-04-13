using Moedelo.AstralV3.Client.AstralInteractionsLogger.Enums;

namespace Moedelo.AstralV3.Client.DAO.DbObjects
{
    /// <summary>
    /// Одна строка из dbo.AstralInteractionMethods
    /// </summary>
    public class AstralInteractionMethod
    {
        /// <summary>
        /// Первичный ключ
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Названием метода (локального или WPF), к которому относится эта строка
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// Режим логирования для данного метода
        /// </summary>
        public MethodLoggingMode Mode { get; set; }
    }
}
