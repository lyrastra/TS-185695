using System.Collections.Generic;

namespace Moedelo.KontragentsV2.Client.DtoWrappers
{
    /// <summary>
    /// Для поддержания контракта старого API в v2-client'е 
    /// </summary>
    public class ListDto<T>
    {
      public List<T> Items { get; set; }
    }
}