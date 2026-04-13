﻿using System.Data;

 namespace Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions.Models
{
    public sealed class DynamicParam
    {
        public DynamicParam(
            string name, 
            object value = null, 
            DbType? dbType = null,
            ParameterDirection? direction = null, 
            int? size = null, 
            byte? precision = null, 
            byte? scale = null)
        {
            Name = name;
            Value = value;
            DbType = dbType;
            Direction = direction;
            Size = size;
            Precision = precision;
            Scale = scale;
        }

        public string Name { get; }

        public object Value { get; }

        public DbType? DbType { get; }

        public ParameterDirection? Direction { get; }

        public int? Size { get; }

        public byte? Precision { get; }

        public byte? Scale { get; }
    }
}