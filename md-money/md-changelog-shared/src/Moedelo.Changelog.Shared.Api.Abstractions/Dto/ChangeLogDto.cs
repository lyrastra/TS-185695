using System;
using System.Collections.Generic;

namespace Moedelo.Changelog.Shared.Api.Abstractions.Dto
{
    public class ChangeLogDto
    {
        public IReadOnlyCollection<Column> Scheme { get; set; }
        public IReadOnlyCollection<Change> Changes { get; set; }
        
        public struct Change
        {
            public DateTime ChangeTime { get; set; }
            public int AuthorUserId { get; set; }
            public IReadOnlyCollection<Field> Fields { get; set; }
        }

        public struct Column
        {
            public string FieldName { get; set; }
            public string FieldTitle { get; set; }
        }

        public struct Field
        {
            public string Name { get; set; }
            public string Type { get; set; }
            public string Value { get; set; }
        }
    }
}
