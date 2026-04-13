using System;
using Moedelo.Common.Enums.Enums.ElectronicReports;

namespace Moedelo.Common.Enums.Attributes
{
    class NeformalDocumentInstructionTypeAttribute : Attribute
    {
        private readonly NeformalDocumentConditionType[] types;

        public NeformalDocumentInstructionTypeAttribute(params NeformalDocumentConditionType[] types)
        {
            this.types = types;
        }

        public NeformalDocumentConditionType[] Types
        {
            get { return types; }
        }
    }
}
