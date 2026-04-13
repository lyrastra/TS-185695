using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.PostgreSqlDataAccess.Abstractions;
using Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions.Extensions;
using Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions.Interface;
using Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions.Models;

namespace Moedelo.Common.PostgreSqlDataAccess.Utils
{
    public static class MoedeloPostgreSqlDbExecutorBaseExtensions
    {
        private class AssemblyRef
        {
        }
        // нужен инстанс, через тип которого можно выйти на текущую сборку (см. реализацию ISqlScriptReader)
        private static readonly AssemblyRef AssemblyRefInstance = new AssemblyRef();

        /// <summary>
        /// Зачистить данные удалённых фирм в БД, на которую указывает dbExecutor
        /// В БД, с которой связан dbExecutor будут найдены все таблицы, удовлетворяющие условиям:
        /// - таблица находится в схеме public
        /// - таблица содержит столбец с именем firm_id
        /// Из найденных таблиц будут удалены все строки, содержащие идентификаторы из списка firmIds
        /// Порядок перебора таблиц неопределён.
        /// </summary>
        /// <param name="dbExecutor">Исполнитель sql-запросов</param>
        /// <param name="scriptReader">сервис чтения sql-запросов из ресурсов</param>
        /// <param name="firmIds">список идентификаторов фирм, для которых должны быть удалены данные</param>
        /// <returns></returns>
        public static Task<IReadOnlyList<TableFirmDataCleanUpResult>> CleanFirmDataAutomaticallyAsync(
            this IMoedeloPostgreSqlDbExecutorBase dbExecutor,
            ISqlScriptReader scriptReader,
            IReadOnlyCollection<int> firmIds)
        {
            return CleanFirmDataAutomaticallyAsync(dbExecutor, scriptReader, firmIds, CancellationToken.None);
        }

        /// <summary>
        /// Зачистить данные удалённых фирм в БД, на которую указывает dbExecutor
        /// В БД, с которой связан dbExecutor будут найдены все таблицы, удовлетворяющие условиям:
        /// - таблица находится в схеме public
        /// - таблица содержит столбец с именем firm_id
        /// Из найденных таблиц будут удалены все строки, содержащие идентификаторы из списка firmIds
        /// Порядок перебора таблиц неопределён.
        /// </summary>
        /// <param name="dbExecutor">Исполнитель sql-запросов</param>
        /// <param name="scriptReader">сервис чтения sql-запросов из ресурсов</param>
        /// <param name="firmIds">список идентификаторов фирм, для которых должны быть удалены данные</param>
        /// <param name="cancellation">токен отмены операции</param>
        /// <returns></returns>
        public static Task<IReadOnlyList<TableFirmDataCleanUpResult>> CleanFirmDataAutomaticallyAsync(
            this IMoedeloPostgreSqlDbExecutorBase dbExecutor,
            ISqlScriptReader scriptReader,
            IReadOnlyCollection<int> firmIds,
            CancellationToken cancellation)
        {
            var sqlScript = scriptReader.Get(
                AssemblyRefInstance,
                "Scripts.CleanUpFirmData.sql");
            var queryParams = new { };
            var tempTable = firmIds.ToTempIntIds("temp_deleted_firm");
            
            var query = new QueryObject(sqlScript, queryParams, tempTable);

            return dbExecutor.QueryAsync<TableFirmDataCleanUpResult>(query, cancellationToken: cancellation);
        }
    }
}
