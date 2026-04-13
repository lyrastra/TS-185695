using System.Collections.Generic;

namespace Moedelo.CommonV2.Cells.Models.Settings
{
    public class ReportGroupSettings
    {
        public ReportGroupSettings()
        {
            Groups = new List<ReportGroup>();
        }

        public List<ReportGroup> Groups { get; set; }
    }
}