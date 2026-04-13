using Moedelo.AstralV3.Client.AstralInteractionsLogger.Enums;
using Moedelo.AstralV3.Client.DAO.DbObjects;
using Moedelo.AstralV3.Client.DAO.Interfaces;
using Moedelo.AstralV3.Client.DAO.SQL;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.DataAccess;
using Moedelo.InfrastructureV2.Domain.Models.DataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.AstralV3.Client.DAO
{
    /// <summary>
    /// Класс для работы с dbo.AstralInteractionMethods
    /// </summary>
    [InjectAsSingleton]
    public class AstralInteractionMethodsDao : IAstralInteractionMethodsDao
    {
        private readonly IMoedeloLogsDbExecutor _db;

        public AstralInteractionMethodsDao(IMoedeloLogsDbExecutor db)
        {
            _db = db;
        }

        public Task<int> Add(string methodName, MethodLoggingMode mode)
        {
            return _db
                .FirstOrDefaultAsync<int>(new QueryObject(SqlScripts.AstralLogger_AddInteractionMethod, new { methodName, mode }));
        }

        public Task<List<AstralInteractionMethod>> Get(List<string> methodNames)
        {
            return _db
                .QueryAsync<AstralInteractionMethod>(new QueryObject(SqlScripts.AstralLogger_GetInteractionMethodsByNames, new { methodNames = methodNames.ToArray() }));
        }

        public Task<List<AstralInteractionMethod>> Get(List<int> ids)
        {
            return _db
                .QueryAsync<AstralInteractionMethod>(new QueryObject(SqlScripts.AstralLogger_GetInteractionMethodsByIds, new { ids = ids.ToArray() }));
        }

        
    }
}
