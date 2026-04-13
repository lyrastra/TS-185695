using Moedelo.Changelog.Shared.Domain.Definitions;

namespace Moedelo.Changelog.Shared.Domain.Attributes
{
    internal sealed class FieldTypeDateTimeAttribute : FieldTypeAttribute
    {
        public FieldTypeDateTimeAttribute() : base(FieldTypes.DateTime)
        {
        }
    }
}
