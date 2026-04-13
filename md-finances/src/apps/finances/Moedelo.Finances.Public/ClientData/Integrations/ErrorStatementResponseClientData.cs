namespace Moedelo.Finances.Public.ClientData.Integrations
{
    public class ErrorStatementResponseClientData : StatementResponseBaseClientData
    {
        public ErrorStatementResponseClientData(string message)
        {
            IsSuccess = false;
            Message = message;
        }
    }
}