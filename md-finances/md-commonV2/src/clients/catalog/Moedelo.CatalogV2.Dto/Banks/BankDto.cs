using IntegrationPartners = Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums.IntegrationPartners;
using Moedelo.Common.Enums.Enums.Integration;

namespace Moedelo.CatalogV2.Dto.Banks
{
    public class BankDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Kpp { get; set; }
        public string Inn { get; set; }
        public string Bik { get; set; }
        public string Address { get; set; }
        public string CorrespondentAccount { get; set; }
        public string FullName { get; set; }
        public string City { get; set; }
        public string FullNameWithCity { get; set; }
        public string Telephone { get; set; }
        public string Index { get; set; }
        public IntegrationPartners? IntegratedPartner { get; set; }
        public bool IsActive { get; set; }
        public int? RegistrationNumber { get; set; }
    }
}