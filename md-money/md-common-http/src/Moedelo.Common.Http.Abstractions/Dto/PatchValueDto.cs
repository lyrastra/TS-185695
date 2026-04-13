namespace Moedelo.Common.Http.Abstractions.Dto
{
    public class PatchValueDto<T>
    {
        public PatchValueDto(T value)
        {
            Value = value;
        }

        public T Value { get; }
    }
}