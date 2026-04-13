using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

using Moedelo.InfrastructureV2.Domain.Models.DataAccess;
// ReSharper disable InvalidXmlDocComment

namespace Moedelo.InfrastructureV2.Domain.Interfaces.DataAccess;

public interface IDbExecutor
{
    Task<List<TR>> QueryAsync<TR>(
        IQueryObject queryObject, 
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    Task<List<TR>> QueryAsync<TR>(
        Dictionary<string, IList> temporaryDataDict, 
        IQueryObject queryObject,
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    Task<List<TR>> QueryAsync<TR>(
        IList<QueryObject> queryObjects, 
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    Task<List<TR>> QueryDistinctAsync<TR>(
        IQueryObject queryObject, 
        Type[] types, 
        Func<object[], TR> map, 
        string splitOn = "Id", 
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);
        
    Task<List<TR>> QueryNoDistinctAsync<TR>(
        IQueryObject queryObject, 
        Type[] types, 
        Func<object[], TR> map, 
        string splitOn = "Id", 
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);
        
    Task<List<TR>> QueryAsync<TR, TT>(
        string tempTableName, 
        List<TT> tempTableData, 
        IQueryObject queryObject, 
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0) where TT : class;

    Task<List<TR>> QueryAsync<TR>(
        IQueryObjectWithDynamicParams queryObject, 
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    /// <summary>
    /// Используется для выполнения запросов с передачей параметров через временные таблицы
    /// </summary>
    /// <param name="temporaryData">Словарь пар название_таблицы:коллекция_объектов. Название пишется без #. Коллекция не надо готовить каким-либо специальным образом</param>
    /// <param name="queryObject"></param>
    /// <param name="settings"></param>
    /// <param name="cancellationToken">токен отмены операции</param>
    /// <returns></returns>
    Task ExecuteAsync(
        IDictionary<string, IList> temporaryData, 
        IQueryObject queryObject, 
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    /// <summary>
    /// Выполнить скрипт
    /// </summary>
    /// <param name="tempTableName">Имя временной таблицы. В запросе следует ссылаться с префиксом #. Например, #MyTableName</param>
    /// <param name="tempTableData">Строки временной таблицы. Названия колонок соответствуют названиям публичных свойств</param>
    /// <param name="queryObject">Конфигурация запроса</param>
    /// <param name="settings">Дополнительные параметры запроса</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <typeparam name="TTempTableRowType">Тип строки временной таблицы</typeparam>
    Task ExecuteAsync<TTempTableRowType>(string tempTableName,
        IEnumerable<TTempTableRowType> tempTableData,
        IQueryObject queryObject,
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0) where TTempTableRowType: class;

    Task<int> ExecuteAsync(
        IQueryObject queryObject, 
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    Task<int> ExecuteAsync(
        IList<QueryObject> queryObjects, 
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    Task ExecuteAsync(
        IQueryObjectWithDynamicParams queryObject, 
        Action<IOutParameterReader> readAction, 
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    Task QueryMultipleAsync(
        IQueryObject queryObject, 
        Func<IGridReader, Task> readAction, 
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    Task<T> QueryMultipleAsync<T>(
        IQueryObject queryObject, 
        Func<IGridReader, Task<T>> readAction, 
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    Task QueryMultipleAsync(
        IQueryObjectWithDynamicParams queryObject, 
        Func<IGridReader, Task> readAction, 
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    Task QueryMultipleAsync(
        IQueryObjectWithDynamicParams queryObject,
        Func<IGridReader, IOutParameterReader, Task> readAction,
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    Task QueryAsync<TR>(
        IQueryObjectWithDynamicParams queryObject, 
        Action<IEnumerable<TR>, IOutParameterReader> readAction, 
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    Task<TDomainResult> QueryAsync<TDbRow, TDomainResult>(
        IQueryObjectWithDynamicParams queryObject, 
        Func<IEnumerable<TDbRow>, IOutParameterReader, TDomainResult> readAction, 
        QuerySetting settings = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    Task<TR> FirstOrDefaultAsync<TR>(
        IQueryObject queryObject, 
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    Task<TR> FirstOrDefaultDistinctAsync<TR>(
        IQueryObject queryObject, 
        Type[] types, 
        Func<object[], TR> map, 
        string splitOn = "Id", 
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);
        
    Task<TR> FirstOrDefaultNoDistinctAsync<TR>(
        IQueryObject queryObject, 
        Type[] types, 
        Func<object[], TR> map, 
        string splitOn = "Id", 
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);
        
    Task<TR> FirstOrDefaultAsync<TR>(
        IList<QueryObject> queryObjects, 
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    Task BulkCopyAsync(
        string table, 
        DataTable data, 
        BulkCopySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    Task QueryAsync<TR>(
        Dictionary<string, IList> temporaryDataDict,
        IQueryObjectWithDynamicParams queryObject, 
        Action<IEnumerable<TR>, IOutParameterReader> readAction,
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    Task<TR> SingleAsync<TR>(
        IQueryObject queryObject, 
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    Task<TR> SingleOrDefaultAsync<TR>(
        IQueryObject queryObject, 
        QuerySetting settings = null,
        CancellationToken? cancellationToken = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);
}