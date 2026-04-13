using Moedelo.Changelog.Shared.Domain.Definitions;

namespace Moedelo.Changelog.Shared.Domain.Attributes
{
    internal sealed class FieldTypeDateAttribute : FieldTypeAttribute
    {
        public FieldTypeDateAttribute() : base(FieldTypes.Date)
        {
        }
    }
    internal sealed class FieldTypeYearAttribute : FieldTypeAttribute
    {
        public FieldTypeYearAttribute() : base(FieldTypes.Year)
        {
        }
    }
}
