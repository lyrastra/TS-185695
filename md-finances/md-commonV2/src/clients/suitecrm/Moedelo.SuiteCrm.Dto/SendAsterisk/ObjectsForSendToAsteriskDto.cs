using System.Collections.Generic;

namespace Moedelo.SuiteCrm.Dto.SendAsterisk
{
    public class ObjectsForSendToAsteriskDto
    {
        public List<ObjectWithValues> Objects { get; set; }
        public ObjectWithValues Buckets { get; set; }
        public ObjectWithValues Filters { get; set; }
        public ObjectWithValues DeclineCases { get; set; }
    }
}