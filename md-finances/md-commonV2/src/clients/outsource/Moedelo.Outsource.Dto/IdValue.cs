namespace Moedelo.Outsource.Dto
{
    public class IdValue<T>
    {
        public T Id { get; set; }
        
        public IdValue(T id)
        {
            Id = id;
        }
    }
}