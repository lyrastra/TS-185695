using System.Collections.Generic;

namespace Moedelo.ErptV2.Dto.EdsRequest
{
    public class EdsRequestDto
    {
        public string Inn { get; set; }
        public string Kpp { get; set; }
        public string Ogrn { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string MobilePhone { get; set; }
        public string ShortName { get; set; }
        public string Address { get; set; }
        public string Vacancy { get; set; }
        public string DirectorInn { get; set; }
        public string Snils { get; set; }
        public string PassportSerial { get; set; }
        public string PassportNumber { get; set; }
        public string PassportDate { get; set; }
        public string PassportDepartment { get; set; }
        public string PassportDepartmentCode { get; set; }
        public string DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public string Gender { get; set; }
        public string Citizenship { get; set; }
        public string Fns { get; set; }
        public string Fsgs { get; set; }
        public string Pfr { get; set; }
        public string PfrNumber { get; set; }
        public string FssNumber { get; set; }
        public string Fss { get; set; }
        public List<AdditionalFnsDto> AdditionalFnsList { get; set; }
        public bool IsFnsAutodecryptionEnabled { get; set; }
    }
}