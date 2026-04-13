using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions.Helpers
{
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
        /// Заменить название параметра списком значений енама.
        /// Метод реализован для поддержки в postgress оператора IN. Осуществляет замену:
        /// :paramName => ('values1', 'values2', ..., 'valuesN'), где values1-valuesN - элементы values, приведенные к строкам
        /// Предполагаемое использование:
        /// SQL input: select * from table where field in :paramName --field имеет тип enum
        /// SQL result: select * from table where field in ('values1', 'values2', ..., 'valuesN')
        /// Метод является альтернативой dapper, который преобразует IEnumerable<T> в массив вида '{val1, val2 ... }'::some_type
        /// что вынуждает использовать конструкцию ANY вместо IN и может ухудшить работу планировщика
        /// </summary>
        /// <param name="paramName">Наименование параметра, который будет заменен списком значений. В sql должен предваряться двоеточием (:paramName)</param>
        /// <param name="values">Список значений TEnum, которые требуется включить в sql-запрос</param>
        /// <param name="condition">Условие выполнения. Замена выполняется, когда condition == true</param>
        /// <typeparam name="TEnum">Enum, значения которого нужно передать в запрос sql. Для корректной работы метода
        /// должен полностью совпадать с enum, созданным на уровне postgress</typeparam>
        public SqlQueryBuilder ReplaceListParamByValueIf<TEnum>(string paramName, IEnumerable<TEnum> values, bool condition)
            where TEnum : Enum
        {
            return condition
                ? ReplaceListParamByValue(paramName, values)
                : this;
        }
        
        /// <summary>
        /// Заменить название параметра списком значений енама.
        /// Метод реализован для поддержки в postgress оператора IN. Осуществляет замену:
        /// :paramName => ('values1', 'values2', ..., 'valuesN'), где values1-valuesN - элементы values, приведенные к строкам
        /// Предполагаемое использование:
        /// SQL input: select * from table where field in :paramName --field имеет тип enum
        /// SQL result: select * from table where field in ('values1', 'values2', ..., 'valuesN')
        /// Метод является альтернативой dapper, который преобразует IEnumerable<T> в массив вида '{val1, val2 ... }'::some_type
        /// что вынуждает использовать конструкцию ANY вместо IN и может ухудшить работу планировщика
        /// </summary>
        /// <param name="paramName">Наименование параметра, который будет заменен списком значений. В sql должен предваряться двоеточием (:paramName)</param>
        /// <param name="values">Список значений TEnum, которые требуется включить в sql-запрос</param>
        /// <typeparam name="TEnum">Enum, значения которого нужно передать в запрос sql. Для корректной работы метода
        /// должен полностью совпадать с enum, созданным на уровне postgress</typeparam>
        public SqlQueryBuilder ReplaceListParamByValue<TEnum>(string paramName, IEnumerable<TEnum> values)
            where TEnum : Enum
        {
            builder.Replace($":{paramName}", $"({string.Join(",", values.Select(v => $"'{v.ToString()}'"))})");

            return this;
        }
        
        /// <summary>
        /// Заменить название параметра списком int.
        /// Метод реализован для поддержки в postgress оператора IN. Осуществляет замену:
        /// :paramName => (values1, values2, ..., valuesN), где values1-valuesN - элементы values, приведенные к строкам
        /// Предполагаемое использование:
        /// SQL input: select * from table where field in :paramName --field имеет тип int
        /// SQL result: select * from table where field in (1, 25, ..., 320)
        /// Метод является альтернативой dapper, который преобразует IEnumerable<T> в массив вида '{val1, val2 ... }'::some_type
        /// что вынуждает использовать конструкцию ANY вместо IN и может ухудшить работу планировщика
        /// </summary>
        /// <param name="paramName">Наименование параметра, который будет заменен списком значений. В sql должен предваряться двоеточием (:paramName)</param>
        /// <param name="values">Список значений int, которые требуется включить в sql-запрос</param>
        /// <param name="condition">Условие выполнения. Замена выполняется, когда condition == true</param>
        public SqlQueryBuilder ReplaceListParamByValueIf(string paramName, IEnumerable<int> values, bool condition)
        {
            return condition
                ? ReplaceListParamByValue(paramName, values)
                : this;
        }
        
        /// <summary>
        /// Заменить название параметра списком int.
        /// Метод реализован для поддержки в postgress оператора IN. Осуществляет замену:
        /// :paramName => (values1, values2, ..., valuesN), где values1-valuesN - элементы values, приведенные к строкам
        /// Предполагаемое использование:
        /// SQL input: select * from table where field in :paramName --field имеет тип int
        /// SQL result: select * from table where field in (1, 25, ..., 320)
        /// Метод является альтернативой dapper, который преобразует IEnumerable<T> в массив вида '{val1, val2 ... }'::some_type
        /// что вынуждает использовать конструкцию ANY вместо IN и может ухудшить работу планировщика
        /// </summary>
        /// <param name="paramName">Наименование параметра, который будет заменен списком значений. В sql должен предваряться двоеточием (:paramName)</param>
        /// <param name="values">Список значений int, которые требуется включить в sql-запрос</param>
        public SqlQueryBuilder ReplaceListParamByValue(string paramName, IEnumerable<int> values)
        {
            builder.Replace($":{paramName}", $"({string.Join(",", values.Select(v =>v.ToString()))})");

            return this;
        }


        /// <summary>
        /// Сформировать результат в виде строки
        /// </summary>
        public override string ToString()
        {
            return builder.ToString();
        }
    }
}