namespace Moedelo.Stock.ApiClient.legacy.models
{
    public class TemplateResponse<T>
    {
        public bool ResponseStatus { get; set; }
        
        public T Value { get; set; }
    }
}