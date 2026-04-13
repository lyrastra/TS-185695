using System;
using System.Collections.Generic;

namespace Moedelo.SpsV2.Dto.Statistics
{
    public class StatRequestByLoginsData
    {
        public List<string> UserLogins { get; set; }
        public DateTime From { get; set; }
        public DateTime? To { get; set; }
    }
}