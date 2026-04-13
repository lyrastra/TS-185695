using Moedelo.AstralV3.Client.DAO.DbObjects;
using System;
using System.Threading.Tasks;

namespace Moedelo.AstralV3.Client.DAO.Interfaces
{
    /// <summary>
    /// Интерфейс для сохранения данных в таблицу dbo.AstralInteractionLogs
    /// </summary>
    public interface IAstralInteractionLogsDao
    {
        /// <summary>
        /// Adds new event to AstralInteractionLogs
        /// </summary>
        Task Add(AstralInteractionLogRecord eventToLog);

        /// <summary>
        /// Removes all logged events, older than thresholdData. Returns deleted events count. Deletes no more than chunkSize records per call.
        /// </summary>
        Task<int> Remove(DateTime thresholdDate, int chunkSize);
    }
}
