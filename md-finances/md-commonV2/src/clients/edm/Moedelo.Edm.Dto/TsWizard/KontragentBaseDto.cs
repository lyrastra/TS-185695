namespace Moedelo.Edm.Dto.TsWizard
{
    /// <summary>
    /// Базовый класс для DTO-шек, связанных с контрагентами
    /// </summary>
    public class KontragentBaseDto
    {
        /// <summary>
        /// Наименование контрагента
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ИНН
        /// </summary>
        public string Inn { get; set; }

        /// <summary>
        /// КПП
        /// </summary>
        public string Kpp { get; set; }

        /// <summary>
        /// Идентификатор провайдера ЭДО
        /// </summary>
        public int EdmSystemId { get; set; }

        /// <summary>
        /// Название оператора ЭДО
        /// </summary>
        public string EdmSystemName { get; set; }
    }
}
