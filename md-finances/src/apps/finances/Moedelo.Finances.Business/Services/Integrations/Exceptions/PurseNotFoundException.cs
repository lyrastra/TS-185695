using System;

namespace Moedelo.Finances.Business.Services.Integrations.Exceptions
{
    public class PurseNotFoundException : Exception
    {
        public PurseNotFoundException(string message) : base(message)
        {

        }
    }
}