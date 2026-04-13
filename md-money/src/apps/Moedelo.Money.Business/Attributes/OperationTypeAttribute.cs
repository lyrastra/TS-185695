using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Business
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    class OperationTypeAttribute : Attribute
    {
        public OperationType OperationType { get; }

        public OperationTypeAttribute(OperationType operationType)
        {
            OperationType = operationType;
        }
    }
}
