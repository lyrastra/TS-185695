namespace Moedelo.Edm.Dto.TsWizard
{
    public class SetEdmLinkResponseDto
    {
        /// <summary>
        /// Успешно-ли установлена связь?
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Пояснение.
        /// </summary>
        public string Message { get; set; }

    }
}
