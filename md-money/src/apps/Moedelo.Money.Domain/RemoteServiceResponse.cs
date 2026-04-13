using Moedelo.Money.Domain.Enums;

namespace Moedelo.Money.Domain
{
    public class RemoteServiceResponse<T>
    {
        public T Data { get; set; }
        public RemoteServiceStatus Status { get; set; }

        public static RemoteServiceResponse<T> ErrorResponse()
        {
            return new RemoteServiceResponse<T>
            {
                Status = RemoteServiceStatus.Error
            };
        }
    }

    public class RemoteServiceResponse
    {
        public static RemoteServiceResponse<T> Error<T>()
        {
            return new RemoteServiceResponse<T>
            {
                Status = RemoteServiceStatus.Error
            };
        }

        public static RemoteServiceResponse<T> Ok<T>(T data)
        {
            return new RemoteServiceResponse<T>
            {
                Data = data,
                Status = RemoteServiceStatus.Ok
            };
        }
    }
}
