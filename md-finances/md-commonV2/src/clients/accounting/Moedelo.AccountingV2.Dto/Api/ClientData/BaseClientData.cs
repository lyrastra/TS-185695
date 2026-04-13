namespace Moedelo.AccountingV2.Dto.Api.ClientData
{
    public class BaseClientData
    {
        public BaseClientData(bool status)
        {
            this.Status = status;
        }

        public BaseClientData()
            : this(true)
        {
        }

        public bool Status { get; set; }
    }
}