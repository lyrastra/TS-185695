using System.Linq;
using Moedelo.Common.Enums.Attributes;
using Moedelo.Common.Enums.Enums.ElectronicReports;

namespace Moedelo.Common.Enums.Extensions.NeformalDocument
{
    public static class NeformalDocumentInstructionTypeExtension
    {
        public static bool ExistsConditionType(this NeformalDocumentInstructionType instructionType, NeformalDocumentConditionType conditionType)
        {
            var attr = instructionType.GetAttribute<NeformalDocumentInstructionTypeAttribute>();
            return attr?.Types?.Contains(conditionType) ?? false;
        }
    }
}
