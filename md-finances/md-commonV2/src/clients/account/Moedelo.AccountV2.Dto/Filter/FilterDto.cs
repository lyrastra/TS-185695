namespace Moedelo.AccountV2.Dto.Filter
{
    public struct FilterDto<TEnumName>
    {
        public TEnumName Name { get; set; }

        public object Value { get; set; }
    }
}