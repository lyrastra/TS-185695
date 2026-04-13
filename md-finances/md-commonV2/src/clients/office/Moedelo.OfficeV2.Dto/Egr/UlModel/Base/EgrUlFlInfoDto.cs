namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Base
{
    /// <summary>
    /// Сведения о ФИО и (при наличии) ИНН ФЛ
    /// </summary>
    public class EgrUlFlInfoDto : EgrUlGrnDateBaseInfoDto
    {
        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string Patronymic { get; set; }

        /// <summary>
        /// ИНН ФЛ
        /// </summary>
        public string InnFl { get; set; }
    }
}
