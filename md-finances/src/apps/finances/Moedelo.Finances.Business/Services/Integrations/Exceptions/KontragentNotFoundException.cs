using System;

namespace Moedelo.Finances.Business.Services.Integrations.Exceptions
{
    public class KontragentNotFoundException : Exception
    {
        public KontragentNotFoundException(string message) : base(message)
        {

        }
    }
}
