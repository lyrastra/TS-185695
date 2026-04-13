namespace Moedelo.Common.Types
{
    public readonly struct RoleId
    {
        private readonly int value;

        public RoleId(int value)
        {
            this.value = value;
        }

        public static explicit operator int (RoleId roleId)
        {
            return roleId.value;
        }

        public static explicit operator RoleId(int value)
        {
            return new RoleId(value);
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
                case RoleId firmId:
                    return Equals(firmId);
            }

            return false;
        }

        public bool Equals(RoleId other)
        {
            return value == other.value;
        }

        public override int GetHashCode()
        {
            return value;
        }
        
        public static bool operator ==(RoleId first, RoleId second)
        {
            return first.value == second.value;
        }

        public static bool operator !=(RoleId first, RoleId second)
        {
            return first.value != second.value;
        }

        public static readonly RoleId Unidentified = new RoleId(-1);
    }
}
