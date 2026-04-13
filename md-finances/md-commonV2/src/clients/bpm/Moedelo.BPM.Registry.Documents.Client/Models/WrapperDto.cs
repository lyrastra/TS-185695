using System.Collections.Generic;

namespace Moedelo.BPM.Registry.Documents.Client.Models
{
    internal class WrapperDto<T>
    {
        public T data { get; set; }
        
        public Dictionary<string, string[]> errors { get; set; }
    }
}