using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.EdsRequest;

namespace Moedelo.ErptV2.Client.EdsApi
{
    public class EdsRequisitesValidateResponse
    {
        public bool IsValid { get; set; }
        public bool IsInvalidSoftValidate { get; set; }
        public List<EdsValidateRequisite> InvalidRequisites { get; set; }
        public List<EdsValidateRequisite> ValidRequisites { get; set; }
        public List<string> ErrorMessages { get; set; } 
        public List<string> WarningMessages { get; set; }
    }
}