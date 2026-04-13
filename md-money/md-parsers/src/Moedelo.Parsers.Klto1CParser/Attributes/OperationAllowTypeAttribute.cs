using System;
using Moedelo.Parsers.Klto1CParser.Enums;

namespace Moedelo.Parsers.Klto1CParser.Attributes
{
    public class OperationAllowTypeAttribute : Attribute
    {
        private readonly OperationType[] types;

        public OperationAllowTypeAttribute(OperationType type)
        {
            types = new[] { type };
        }

        public OperationAllowTypeAttribute(OperationType[] types)
        {
            this.types = types;
        }

        public OperationType[] GetOperatonTypes()
        {
            return types;
        }
    }
}