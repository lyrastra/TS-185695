using System;
using System.Collections.Generic;

namespace Moedelo.CommonV2.Auth.OAuth2.Models
{
    public class MdClaims
    {
        public int UserId { get; set; }

        public int FirmId { get; set; }

        public int ClientId { get; set; }

        public List<string> Scopes { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public bool Temporary { get; set; }

        public Guid Guid { get; set; }
    }
}