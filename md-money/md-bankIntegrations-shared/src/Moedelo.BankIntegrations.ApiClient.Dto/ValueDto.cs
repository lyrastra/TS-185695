namespace Moedelo.BankIntegrations.ApiClient.Dto
{
    public class ValueDto<TValue>
    {
        public ValueDto()
        {
            Value = default;
        }
        
        public ValueDto(TValue value)
        {
            Value = value;
        }

        public TValue Value { get; set; }
    }
}
