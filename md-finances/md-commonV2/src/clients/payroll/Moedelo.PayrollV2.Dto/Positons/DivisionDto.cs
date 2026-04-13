using Moedelo.Common.Enums.Enums.Accounting;

namespace Moedelo.PayrollV2.Dto.Positions
{
    public class DivisionDto
    {
        public int Id { get; set; }

        /// <summary>
        /// Фирма, к которой относится отдел
        /// </summary>
        public int FirmId { get; set; }

        /// <summary>
        /// Название отдела
        /// </summary>
        public string Name { get; set; }

        public long? SubcontoId { get; set; }

        public SubcontoType SubcontoType
        {
            get { return SubcontoType.SeparateDivision; }
        }

        public string SubcontoName
        {
            get { return Name; }
        }
    }
}