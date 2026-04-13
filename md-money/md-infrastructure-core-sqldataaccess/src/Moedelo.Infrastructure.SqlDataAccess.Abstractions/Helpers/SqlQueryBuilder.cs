using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Moedelo.Infrastructure.SqlDataAccess.Abstractions.Helpers;

public sealed class SqlQueryBuilder
{
    private readonly StringBuilder builder;

    public SqlQueryBuilder(string sqlTemplate)
    {
        if (string.IsNullOrWhiteSpace(sqlTemplate))
        {
            throw new ArgumentException("Sql template cannot be null or empty", nameof(sqlTemplate));
        }

        builder = new StringBuilder(sqlTemplate);
    }

    /// <summary>
    /// Включить блок sql на одной строке с комментарием --lineName-- по условию
    /// </summary>
    public SqlQueryBuilder IncludeLineIf(string lineName, bool condition)
    {
        return condition ? IncludeLine(lineName) : this;
    }

    /// <summary>
    /// Включить блок sql на одной строке с комментарием --lineName--
    /// </summary>
    public SqlQueryBuilder IncludeLine(string lineName)
    {
        builder.Replace($"--{lineName}--", string.Empty);
        return this;
    }

    /// <summary>
    /// Включить блок sql внутри многострострочного комментария /*--blockName-- ... --blockName--*/ по условию
    /// </summary>
    public SqlQueryBuilder IncludeBlockIf(string blockName, bool condition)
    {
        return condition ? IncludeBlock(blockName) : this;
    }

    /// <summary>
    /// Включить блок sql внутри многострострочного комментария /*--blockName-- ... --blockName--*/
    /// </summary>
    public SqlQueryBuilder IncludeBlock(string blockName)
    {
        builder.Replace($"/*--{blockName}--", string.Empty);
        builder.Replace($"--{blockName}--*/", string.Empty);

        return this;
    }

    /// <summary>
    /// Заменить вхождение целочисленного параметра на значение
    /// </summary>
    public SqlQueryBuilder ReplaceParamByValue(string paramName, int value)
    {
        ReplaceParamByValueInternal(paramName, value.ToString());
        return this;
    }

    /// <summary>
    /// Заменить вхождение целочисленного параметра на значение
    /// </summary>
    public SqlQueryBuilder ReplaceParamByValue(string paramName, long value)
    {
        ReplaceParamByValueInternal(paramName, value.ToString());
        return this;
    }

    /// <summary>
    /// Внутренний метод для замены параметров с точным совпадением
    /// </summary>
    private void ReplaceParamByValueInternal(string paramName, string value)
    {
        if (string.IsNullOrWhiteSpace(paramName))
        {
            throw new ArgumentException("Parameter name cannot be null or empty", nameof(paramName));
        }

        // Экранируем специальные символы regex в имени параметра
        var escapedParamName = Regex.Escape(paramName);
        
        // Используем regex для точного совпадения параметра с границами слов
        // @paramName должен быть либо в начале строки, либо после символа, не являющегося буквой/цифрой/подчеркиванием
        var pattern = $@"@\b{escapedParamName}\b";
        
        var result = Regex.Replace(builder.ToString(), pattern, value);
        builder.Clear();
        builder.Append(result);
    }

    /// <summary>
    /// Сформировать результат в виде строки
    /// </summary>
    public override string ToString()
    {
        return builder.ToString();
    }
}