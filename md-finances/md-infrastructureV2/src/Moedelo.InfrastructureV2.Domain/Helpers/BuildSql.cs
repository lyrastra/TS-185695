using System;
using System.Text;
using Moedelo.InfrastructureV2.Domain.Models.DataAccess;

namespace Moedelo.InfrastructureV2.Domain.Helpers;

/// <summary>
/// Формирует sql из шаблона путем раскомментирования одно- и много- строчных комментариев по условию
/// </summary>
public class BuildSql
{
    private StringBuilder builder;

    /// <summary>
    /// Взять строчку в качестве шаблона sql 
    /// </summary>
    public static BuildSql From(string sqlTemplate)
    {
        if (sqlTemplate == null)
        {
            throw new ArgumentNullException(nameof(sqlTemplate));
        }
            
        return new BuildSql
        {
            builder = new StringBuilder(sqlTemplate)
        };
    }

    /// <summary>
    /// Включить блок sql на одной строке с комментарием --lineName-- по условию
    /// </summary>
    public BuildSql IncludeLineIf(string lineName, bool condition)
    {
        return condition ? IncludeLine(lineName) : this;
    }

    /// <summary>
    /// Включить несколько однострочных блоков sql (по комментариям --lineNames[i]--) по условию
    /// </summary>
    public BuildSql IncludeLinesIf(bool condition, params string[] lineNames)
    {
        if (condition)
        {
            foreach (var lineName in lineNames)
            {
                builder.Replace($"--{lineName}--", string.Empty);
            }
        }

        return this;
    }

    /// <summary>
    /// Включить блок sql на одной строке с комментарием --lineName--
    /// </summary>
    public BuildSql IncludeLine(string lineName)
    {
        builder.Replace($"--{lineName}--", string.Empty);
        return this;
    }

    /// <summary>
    /// Включить блок sql на одной строке с комментарием --func.Result--
    /// </summary>
    public BuildSql IncludeLine(Func<string> func)
    {
        builder.Replace($"--{func()}--", string.Empty);
        return this;
    }

    /// <summary>
    /// Включить блок sql внутри многострострочного комментария /*--blockName-- ... --blockName--*/ по условию
    /// </summary>
    public BuildSql IncludeBlockIf(string blockName, bool condition)
    {
        return condition ? IncludeBlock(blockName) : this;
    }

    /// <summary>
    /// Включить блок sql внутри многострострочного комментария /*--blockName-- ... --blockName--*/
    /// </summary>
    public BuildSql IncludeBlock(string blockName)
    {
        builder.Replace($"/*--{blockName}--", string.Empty);
        builder.Replace($"--{blockName}--*/", string.Empty);

        return this;
    }

    /// <summary>
    /// Сформировать результат в виде QueryObject
    /// </summary>
    public QueryObject ToQueryObject(object queryParams = null)
    {
        return new QueryObject(builder.ToString(), queryParams);
    }
        
    /// <summary>
    /// Сформировать результат в виде строки
    /// </summary>
    public override string ToString()
    {
        return builder.ToString();
    }
}