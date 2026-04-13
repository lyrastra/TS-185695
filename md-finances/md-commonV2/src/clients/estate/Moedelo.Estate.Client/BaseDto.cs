namespace Moedelo.Estate.Client
{
    public class BaseDto
    {
        public BaseDto()
        {
            ResponseStatus = true;
            ResponseMessage = null;
        }

        public BaseDto(bool status, string message = null)
        {
            ResponseStatus = status;
            ResponseMessage = message;
        }

        public bool ResponseStatus { get; set; }

        public string ResponseMessage { get; set; }
    }
}