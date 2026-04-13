using Moedelo.AstralV3.Client.DAO.DbObjects;
using Moedelo.AstralV3.Client.DAO.Interfaces;
using Moedelo.AstralV3.Client.DAO.SQL;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.DataAccess;
using Moedelo.InfrastructureV2.Domain.Models.DataAccess;
using System;
using System.Threading.Tasks;

namespace Moedelo.AstralV3.Client.DAO
{
    [InjectAsSingleton(typeof(IAstralInteractionLogsDao))]
    public class AstralInteractionLogsDao : IAstralInteractionLogsDao
    {
        private readonly IMoedeloLogsDbExecutor _db;

        public AstralInteractionLogsDao(IMoedeloLogsDbExecutor db)
        {
            _db = db;
        }

        public Task Add(AstralInteractionLogRecord eventToLog)
        {
            return _db.ExecuteAsync(new QueryObject(SqlScripts.AstralLogger_AddInteractionLog, eventToLog));
        }

        public Task<int> Remove(DateTime thresholdDate, int chunkSize)
        {
            return _db.ExecuteAsync(new QueryObject(SqlScripts.AstralLogger_RemoveInteractionLogs, new { thresholdDate, chunkSize }));
        }
    }
}
