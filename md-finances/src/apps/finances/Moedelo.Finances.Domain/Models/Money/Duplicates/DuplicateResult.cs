namespace Moedelo.Finances.Domain.Models.Money.Duplicates
{
    public class DuplicateResult
    {
        public long? Id { get; set; }

        public long? BaseId { get; set; }

        public bool IsStrict { get; set; }
    }
}