using System.Collections.Generic;

namespace Moedelo.CommonV2.Cells.Models.Settings
{
    public class ReportColorSettings
    {
        public ReportColorSettings()
        {
            Colors = new Dictionary<System.Drawing.Color, Color>();
        }

        public Dictionary<System.Drawing.Color, Color> Colors { get; set; }
    }
}