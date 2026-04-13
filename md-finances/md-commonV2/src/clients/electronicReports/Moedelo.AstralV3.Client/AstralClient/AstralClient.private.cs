using Moedelo.AstralV3.Client.AstralClientsFactory;
using Moedelo.AstralV3.Client.AstralInteractionsLogger.Interfaces;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moedelo.InfrastructureV2.Domain.Interfaces.Redis;
using System;

namespace Moedelo.AstralV3.Client.AstralClient
{
    /// <summary>
    /// Часть класса AstralClient, содержащая приватные методы
    /// </summary>
    [InjectAsSingleton]
    public partial class AstralClient : IAstralClient
    {
        private const string Login = "Astral";
        private const string Password = "BhGFD7129ApDrEE344cvX";
        private const string AstralTokenName = "astralWcfToken";
        private readonly TimeSpan AstralTokenLifeTime = TimeSpan.FromMinutes(30);

        private readonly ILogger _logger;
        private readonly IDefaultRedisDbExecuter _redisDbExecuter;
        private readonly IAstralClientsFactory _astralClientsFactory;
        private readonly ILogsWriter _logsWriter;

        /// <summary>
        /// Клиент для получения сессии по сертификату
        /// </summary>

        private const string _loggerTag = nameof(AstralClient);

        public AstralClient(ILogger logger, IDefaultRedisDbExecuter redisDbExecuter, IAstralClientsFactory astralClientsFactory,
            ILogsWriter logsWriter)
        {
            _logger = logger;
            _redisDbExecuter = redisDbExecuter;
            _astralClientsFactory = astralClientsFactory;
            _logsWriter = logsWriter;
        }
    }
}
