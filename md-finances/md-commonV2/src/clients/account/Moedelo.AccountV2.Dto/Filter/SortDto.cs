namespace Moedelo.AccountV2.Dto.Filter
{
    public struct SortDto<TEnumName>
    {
        public TEnumName Name { get; set; }

        public SortType Value { get; set; } 
    }
}