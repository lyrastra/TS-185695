using System;

namespace Moedelo.Money.Enums.Attributes
{
    public class OperationKindAttribute : Attribute
    {
        public OperationKindAttribute(OperationKind kind)
        {
            Kind = kind;
        }

        public OperationKind Kind { get; }
    }
}