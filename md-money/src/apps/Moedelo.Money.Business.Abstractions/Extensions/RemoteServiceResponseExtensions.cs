using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Domain;
using Moedelo.Money.Domain.Enums;
using System;

namespace Moedelo.Money.Business.Abstractions.Extensions
{
    public static class RemoteServiceResponseExtensions
    {
        public static T GetOrThrow<T>(this RemoteServiceResponse<T> response)
        {
            if (response == null)
            {
                throw new ArgumentNullException();
            }
            if (response.Status != RemoteServiceStatus.Ok)
            {
                throw new RemoteServiceException();
            }
            return response.Data;
        }
    }
}
