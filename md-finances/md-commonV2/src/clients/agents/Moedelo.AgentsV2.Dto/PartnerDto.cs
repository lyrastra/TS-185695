using System;
using Moedelo.Common.Enums.Enums.Partners;

namespace Moedelo.AgentsV2.Dto
{
    public class PartnerDto
    {
        public int? Id { get; set; }

        public bool FromSite { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int? ParentId { get; set; }
        
        public string Phone { get; set; }
        
        public string FullName { get; set; }
    }
}