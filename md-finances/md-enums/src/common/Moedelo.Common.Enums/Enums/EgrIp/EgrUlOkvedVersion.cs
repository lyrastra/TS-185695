using System.Xml.Serialization;

namespace Moedelo.Common.Enums.Enums.EgrIp
{
    /// <summary>
    /// Признак версии Общероссийского классификатора видов экономической деятельности.
    /// Отсутствие данного атрибута в файле означает, что при внесении кода ОКВЭД использовался классификатор ОК 029-2001 (КДЕС Ред. 1)
    /// </summary>
    public enum EgrUlOkvedVersion
    {
        /// <summary>
        /// код соответствует версии ОКВЭД ОК 029-2001 (КДЕС Ред. 1)
        /// </summary>
        [XmlEnum("2001")]
        Version2001,

        /// <summary>
        /// код соответствует версии ОКВЭД ОК 029-2014 (КДЕС Ред. 2)
        /// </summary>
        [XmlEnum("2014")]
        Version2014
    }
}