namespace Moedelo.CommonV2.Utils.ServerUrl
{
    public class DomainParts
    {
        public string Level1 { get; set; }
        public string Level2 { get; set; }
        public string Level3 { get; set; }
        public string MachineNumber { get; set; }

        public string BuildFullName()
        {
            return $"{Level3}{MachineNumber}.{Level2}.{Level1}";
        }
    }
}
