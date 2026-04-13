namespace Moedelo.ErptV2.Dto
{
    public class BaseDto
    {
        public BaseDto()
        {
            this.ResponseStatus = true;
            this.ResponseMessage = (string) null;
        }

        public BaseDto(bool status, string message = null)
        {
            this.ResponseStatus = status;
            this.ResponseMessage = message;
        }

        public bool ResponseStatus { get; set; }

        public string ResponseMessage { get; set; }
    }
}