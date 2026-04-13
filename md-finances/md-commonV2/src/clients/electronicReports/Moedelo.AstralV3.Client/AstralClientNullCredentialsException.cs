using System;

namespace Moedelo.AstralV3.Client
{
    public class AstralClientNullCredentialsException : Exception
    {
        public AstralClientNullCredentialsException()
            : base("Ошибка инициализации клиента. ClientCredentials are null")
        {
        }
    }
}