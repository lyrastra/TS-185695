using Moedelo.Accounts.ApiClient.Enums;

namespace Moedelo.Accounts.Abstractions.Dto
{
    public class FirmLeadMarkDto
    {
        public int FirmId { get; set; }

        public LeadMarkType LeadMarkType { get; set; }

        public int LeadMarkId { get; set; }

        public int OperatorId => LeadMarkType == LeadMarkType.Operator
            ? LeadMarkId
            : 0;

        public int RegionalPartnerId => LeadMarkType == LeadMarkType.Partner || LeadMarkType == LeadMarkType.Franchisee
            ? LeadMarkId
            : 0;

        public int ProfOutsourceId => LeadMarkType == LeadMarkType.OutsourcingMdPartner || LeadMarkType == LeadMarkType.Account
            ? LeadMarkId
            : 0;
    }
}