using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Aspose.Words.MailMerging;

namespace Moedelo.Infrastructure.Aspose.Word.Internals
{
    internal class MailMergeDataSource : IMailMergeDataSource
    {
        private readonly IEnumerator mEnumerator;
        private object mCurrentObject;

        public string TableName { get; } = string.Empty;

        public MailMergeDataSource(IEnumerable data)
        {
            mEnumerator = data.GetEnumerator();
        }

        public MailMergeDataSource(IEnumerable data, string tableName)
        {
            mEnumerator = data.GetEnumerator();
            TableName = tableName;
        }

        public bool GetValue(string fieldName, out object fieldValue)
        {
            fieldValue = GetPropertyValue(fieldName);

            return fieldValue != null;
        }

        public IMailMergeDataSource GetChildDataSource(string tableName)
        {
            object nestedData = GetPropertyValue(tableName);

            if (nestedData is IEnumerable)
            {
                if (IsPrimitiveTypeCollection(nestedData.GetType()))
                {
                    var result = ((IEnumerable<object>)nestedData).Select(x => new { Value = x });

                    return new MailMergeDataSource(result);
                }

                return new MailMergeDataSource((IEnumerable)nestedData);
            }

            return null;
        }

        public bool MoveNext()
        {
            bool hasNexeRecord = mEnumerator.MoveNext();

            if (hasNexeRecord)
            {
                mCurrentObject = mEnumerator.Current;
            }

            return hasNexeRecord;
        }

        private object GetPropertyValue(string propertyName)
        {
            var splitedProperties = propertyName.Split(new []{'.'}, StringSplitOptions.RemoveEmptyEntries);
            object propertyValue = null;
            var currentObject = mCurrentObject;

            foreach (var splitedProperty in splitedProperties)
            {
                Type curentRecordType = currentObject.GetType();
                PropertyInfo property = curentRecordType.GetProperty(splitedProperty);

                if (property != null)
                {
                    propertyValue = property.GetValue(currentObject, null) ?? string.Empty;
                    currentObject = propertyValue;
                }
            }

            return propertyValue;
        }

        public static bool IsPrimitiveTypeCollection(Type type)
        {
            return type.IsGenericType && type.GenericTypeArguments.Length == 1 && IsSimpleType(type.GenericTypeArguments[0]);
        }

        public static bool IsSimpleType(Type type)
        {
            return
                type.IsPrimitive ||
                new []
                {
                    typeof (Enum),
                    typeof (string),
                    typeof (decimal),
                    typeof (DateTime),
                    typeof (DateTimeOffset),
                    typeof (TimeSpan),
                    typeof (Guid)
                }.Contains(type) ||
                Convert.GetTypeCode(type) != TypeCode.Object ||
                (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>) && IsSimpleType(type.GetGenericArguments()[0]));
        }
    }
}