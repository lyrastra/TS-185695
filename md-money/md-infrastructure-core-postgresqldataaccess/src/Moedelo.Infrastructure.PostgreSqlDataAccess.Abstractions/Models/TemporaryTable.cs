using System;
using System.Data;

namespace Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions.Models
{
    public sealed class TemporaryTable
    {
        public TemporaryTable(string name, string createTableSql, DataTable dataTable)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (name.StartsWith("temp_") == false)
            {
                throw new ArgumentException("Name of temporary table should start with 'temp_'");
            }

            if (string.IsNullOrEmpty(createTableSql))
            {
                throw new ArgumentNullException(nameof(createTableSql));
            }

            Name = name;
            CreateSql = createTableSql;
            DataTable = dataTable ?? throw new ArgumentNullException(nameof(dataTable));
        }

        public string Name { get; }
        
        public string CreateSql { get; }

        public string DropSql => $"drop table {Name}";
        
        public DataTable DataTable { get; }
    }
}