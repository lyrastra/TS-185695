using Moedelo.CommonV2.Cells.Enums;

namespace Moedelo.CommonV2.Cells.Models.Settings
{
    public class RangeSettings
    {
        public static RangeSettings Table(string name)
        {
            return new RangeSettings {Name = name, MinHeight = 18, IsAutoFit = true};
        }

        public static RangeSettings Text(string name)
        {
            return new RangeSettings {Name = name, MinHeight = 12, IsAutoFit = true};
        }

        public string Name { get; set; }

        public bool IsAutoFit { get; set; }

        public int MinHeight { get; set; }

        public bool Hide { get; set; }

        public DeletionType DeletionType { get; set; }
    }
}