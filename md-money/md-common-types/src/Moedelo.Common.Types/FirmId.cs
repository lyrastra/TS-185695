namespace Moedelo.Common.Types
{
    public readonly struct FirmId
    {
        private readonly int value;

        public FirmId(int value)
        {
            this.value = value;
        }

        public static explicit operator int(FirmId firmId)
        {
            return firmId.value;
        }

        public static explicit operator FirmId(int value)
        {
            return new FirmId(value);
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case null:
                    return false;
                case FirmId firmId:
                    return Equals(firmId);
            }

            return false;
        }

        public bool Equals(FirmId other)
        {
            return value == other.value;
        }

        public override int GetHashCode()
        {
            return value;
        }

        public static bool operator ==(FirmId first, FirmId second)
        {
            return first.value == second.value;
        }

        public static bool operator !=(FirmId first, FirmId second)
        {
            return first.value != second.value;
        }
        
        public static readonly FirmId Unidentified = new FirmId(-1);
    }
}