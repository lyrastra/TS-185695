namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Base
{
    /// <summary>
    /// Сведенияо о адресообразующем элементе город
    /// </summary>
    public class EgrUlCityInfoDto
    {
        /// <summary>
        /// Тип элемента город (волость и т.п.)
        /// Принимает значение полного наименования типа адресного объекта в соответствии с Классификатором адресов России (КЛАДР)
        /// </summary>
        public string Type { get; set; }
        
        /// <summary>
        /// Наименование (элемент город)
        /// </summary>
        public string Name { get; set; }
    }
}
