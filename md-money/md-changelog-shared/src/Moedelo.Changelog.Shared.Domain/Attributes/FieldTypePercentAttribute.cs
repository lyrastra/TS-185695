using Moedelo.Changelog.Shared.Domain.Definitions;

namespace Moedelo.Changelog.Shared.Domain.Attributes
{
    internal sealed class FieldTypePercentAttribute : FieldTypeAttribute
    {
        public FieldTypePercentAttribute() : base(FieldTypes.Percent)
        {
        }
    }
}
