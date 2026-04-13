namespace Moedelo.SuiteCrm.Dto.User
{
    public class CrmUserCriteriaDto
    {
        /// <summary>
        /// Только активные
        /// </summary>
        public bool OnlyActive { get; set; }

        /// <summary>
        /// Только операторы
        /// </summary>
        public bool OnlyMdOperators { get; set; }

        /// <summary>
        /// Смещение начальной позиции чтения
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// Количество записей на странице
        /// </summary>
        public int Limit { get; set; }
    }
}
