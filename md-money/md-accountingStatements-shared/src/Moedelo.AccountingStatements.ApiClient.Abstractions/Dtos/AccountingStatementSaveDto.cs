namespace Moedelo.AccountingStatements.ApiClient.Abstractions.Dtos
{
    public class AccountingStatementSaveDto
    {
        public long Id { get; set; }
        public long? DocumentBaseId { get; set; }
    }
}