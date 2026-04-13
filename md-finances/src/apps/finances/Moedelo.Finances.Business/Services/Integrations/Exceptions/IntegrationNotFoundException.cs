using System;

namespace Moedelo.Finances.Business.Services.Integrations.Exceptions
{
    public class IntegrationNotFoundException : Exception
    {
        public IntegrationNotFoundException() : base()
        {

        }

        public IntegrationNotFoundException(string message) : base(message)
        {

        }
    }
}