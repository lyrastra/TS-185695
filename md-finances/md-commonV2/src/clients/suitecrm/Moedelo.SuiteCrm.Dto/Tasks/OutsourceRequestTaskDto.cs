namespace Moedelo.SuiteCrm.Dto.Tasks
{
    public class OutsourceRequestTaskDto
    {
        public int FirmId { get; set; }

        /// <summary>
        /// Признак "заявка из онбординга"
        /// </summary>
        public bool IsOnboarding { get; set; }

        /// <summary>
        /// Выбросить исключение или вернуть статус "неуспешно"
        /// </summary>
        public bool ThrowOnFailure { get; set; }
    }
}
