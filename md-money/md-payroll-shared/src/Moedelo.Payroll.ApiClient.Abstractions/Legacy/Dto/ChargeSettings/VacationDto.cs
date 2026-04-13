using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeSettings
{
    public class VacationDto
    {
        public int WorkerId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public VacationType Type { get; set; }


        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != GetType())
            {
                return false;
            }
            return Equals((VacationDto)obj);
        }

        private bool Equals(VacationDto other)
        {
            return WorkerId == other.WorkerId
                && StartDate == other.StartDate
                && EndDate == other.EndDate
                && Type == other.Type;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = WorkerId.GetHashCode();
                hashCode = (hashCode * 397) ^ StartDate.GetHashCode();
                hashCode = (hashCode * 397) ^ EndDate.GetHashCode();
                hashCode = (hashCode * 397) ^ Type.GetHashCode();
                return hashCode;
            }
        }
    }
}
