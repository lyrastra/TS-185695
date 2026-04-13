using System;
using Moedelo.Common.Enums.Enums.Accounting;

namespace Moedelo.PayrollV2.Dto.Positions
{
    public class WorkerPositionFullDto 
    {
        public int Id { get; set; }

        public int WorkerId { get; set; }

        public int PositionId { get; set; }

        public int TypeId { get; set; }

        public int DivisionId { get; set; }

        public string PositionName { get; set; }

        public string PositionKey { get; set; }

        public string DivisionName { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime StartDate { get; set; }

        public string Debet { get; set; }

        public long? SubcontoId { get; set; }

        public virtual SubcontoType SubcontoType
        {
            get { return SubcontoType.SeparateDivision; }
        }

        public string SubcontoName
        {
            get { return DivisionName; }
        }
    }
}