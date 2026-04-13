namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    /// <summary>
    /// Сведения о территориальном органе Пенсионного фонда Российской Федерации
    /// </summary>
    public class EgrUlPfInfoDto
    {
        /// <summary>
        /// Код по справочнику СТОПФ
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }
    }
}