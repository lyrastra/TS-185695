namespace Moedelo.Common.Types
{
    public readonly struct UserId
    {
        private readonly int value;

        public UserId(int value)
        {
            this.value = value;
        }

        public static explicit operator int (UserId userId)
        {
            return userId.value;
        }

        public static explicit operator UserId(int value)
        {
            return new UserId(value);
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
                case UserId firmId:
                    return Equals(firmId);
            }

            return false;
        }

        public bool Equals(UserId other)
        {
            return value == other.value;
        }

        public override int GetHashCode()
        {
            return value;
        }
        
        public static bool operator ==(UserId first, UserId second)
        {
            return first.value == second.value;
        }

        public static bool operator !=(UserId first, UserId second)
        {
            return first.value != second.value;
        }
        
        public static readonly UserId Unidentified = new UserId(-1);
    }
}
