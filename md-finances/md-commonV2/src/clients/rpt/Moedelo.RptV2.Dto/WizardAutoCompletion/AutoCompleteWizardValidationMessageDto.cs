namespace Moedelo.RptV2.Dto.WizardAutoCompletion
{
    public class AutoCompleteWizardValidationMessageDto
    {
        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Номер шага
        /// </summary>
        public byte? StepNumber { get; set; }

        /// <summary>
        /// Тип ошибки
        /// </summary>
        public AutoCompleteWizardMessageType MessageType { get; set; }
    }

    public enum AutoCompleteWizardMessageType
    {
        /// <summary>
        /// Ошибка прохождения мастера
        /// </summary>
        General = 1,
        /// <summary>
        /// Не подключена подпись
        /// </summary>
        InvalidSignature,
        /// <summary>
        /// Ошибка валидации
        /// </summary>
        Validation
    }
}