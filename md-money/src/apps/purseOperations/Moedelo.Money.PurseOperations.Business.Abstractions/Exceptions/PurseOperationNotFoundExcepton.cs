using System;

namespace Moedelo.Money.PurseOperations.Business.Abstractions.Exceptions
{
    public class PurseOperationNotFoundExcepton : Exception
    {
        public long DocumentBaseId { get; set; }
    }
}
