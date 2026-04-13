using System.Xml.Serialization;
using Moedelo.Common.Enums.Enums.EgrIp;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base
{
    [XmlType(AnonymousType = true)]
    public class EgrUlOkved : EgrUlGrnDateBaseInfo
    {
        /// <summary>
        /// Код по Общероссийскому классификатору видов экономической деятельности
        /// </summary>
        [XmlAttribute("КодОКВЭД")]
        public string Code { get; set; }

        /// <summary>
        /// Наименование вида деятельности по Общероссийскому классификатору видов экономической деятельности
        /// </summary>
        [XmlAttribute("НаимОКВЭД")]
        public string Name { get; set; }


        /// <summary>
        /// Признак версии Общероссийского классификатора видов экономической деятельности
        /// </summary>
        [XmlAttribute("ПрВерсОКВЭД")]
        public EgrUlOkvedVersion Version { get; set; }
    }
}
