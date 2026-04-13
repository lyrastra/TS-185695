using System;
using System.Collections.Generic;

namespace Moedelo.SuiteCrm.Dto.SendAsterisk
{
    public class ObjectWithValues
    {
        public string ObjectTypeId { get; set; }
        public string Name { get; set; }
        public List<KeyValuePair<string, string>> Values { get; set; }
    }
}