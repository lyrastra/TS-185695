using System.Collections.Generic;

namespace Moedelo.ContractsV2.Client.DtoWrappers
{
    /// <summary>
    /// Для поддержания контракта старого API в v2-client'е 
    /// </summary>
    internal class ListDto<T> where T : class, new()
    {
        public List<T> Items { get; set; }
    }
}