namespace Moedelo.Finances.WebApp.ClientData.Integrations
{
    public class ErrorStatementResponseClientData
    {
        public ErrorStatementResponseClientData(string message)
        {
            IsSuccess = false;
            Message = message;
        }

        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}