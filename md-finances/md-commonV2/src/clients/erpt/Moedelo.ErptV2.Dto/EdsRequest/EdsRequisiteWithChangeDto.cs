using Moedelo.Common.Enums.Enums.EdsRequest;

namespace Moedelo.ErptV2.Dto.EdsRequest
{
    public sealed class EdsRequisiteWithChangeDto
    {
        public EdsRequestField FieldType { get; }
        public string Value { get; }
        public string PreviousValue { get; }
        public EdsRequestChangeType ChangeType { get; }


        public EdsRequisiteWithChangeDto(EdsRequestField fieldType, string value, string previousValue, EdsRequestChangeType changeType)
        {
            FieldType = fieldType;
            Value = value;
            PreviousValue = previousValue;
            ChangeType = changeType;
        }
    }
}