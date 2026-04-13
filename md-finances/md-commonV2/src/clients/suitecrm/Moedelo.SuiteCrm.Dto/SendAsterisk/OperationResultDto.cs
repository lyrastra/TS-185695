namespace Moedelo.SuiteCrm.Dto.SendAsterisk
{
    public class OperationResultDto
    {
        /// <summary>
        /// всего собрано по заданным критериям отправки
        /// </summary>
        public int CountAll { get; set; }
        /// <summary>
        /// подготовлено к отправке (некоторые лиды могут быть отбракованы в процессе подготовки к отправке)
        /// </summary>
        public int CountSentToApi { get; set; }
        /// <summary>
        /// принято в автодозвон (некоторые лиды могут быть отвергнуты автообзвоном по каким-либо причинам)
        /// </summary>
        public int CountSuccess { get; set; }
    }
}