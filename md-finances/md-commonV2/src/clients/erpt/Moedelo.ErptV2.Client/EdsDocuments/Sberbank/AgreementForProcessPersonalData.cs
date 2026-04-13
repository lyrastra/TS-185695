using System;

namespace Moedelo.ErptV2.Client.EdsDocuments.Sberbank
{
    public class AgreementForProcessPersonalData
    {
        public string DirectorName { get; set; }
        public string DirectorPatronymic { get; set; }
        public string DirectorSurname { get; set; }

        public string PassportSeries { get; set; }
        public string PassportNumber { get; set; }
        public string PassportIssuer { get; set; }
        public DateTime PassportIssueDate { get; set; }
        public string DivisionCode { get; set; }
        public string BirthPlace { get; set; }
        public DateTime BirthDate { get; set; }
        
        public string Address { get; set; }

        public bool IsOoo { get; set; }
        public string Inn { get; set; }
        public string Kpp { get; set; }
        public string Ogrn { get; set; }
        public string Email { get; set; }
        
        public string DirectorPosition { get; set; }
        public string OrganisationNameShort { get; set; }
    }
}
