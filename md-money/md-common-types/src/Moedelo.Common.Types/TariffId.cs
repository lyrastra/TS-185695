namespace Moedelo.Common.Types
{
    public readonly struct TariffId
    {
        private readonly int value;

        public TariffId(int value)
        {
            this.value = value;
        }

        public static explicit operator int (TariffId tariffId)
        {
            return tariffId.value;
        }

        public static explicit operator TariffId(int value)
        {
            return new TariffId(value);
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
                case TariffId firmId:
                    return Equals(firmId);
            }

            return false;
        }

        public bool Equals(TariffId other)
        {
            return value == other.value;
        }

        public override int GetHashCode()
        {
            return value;
        }
        
        public static bool operator ==(TariffId first, TariffId second)
        {
            return first.value == second.value;
        }

        public static bool operator !=(TariffId first, TariffId second)
        {
            return first.value != second.value;
        }
        
        public static readonly TariffId Unidentified = new TariffId(-1);
    }
}
