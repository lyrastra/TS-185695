namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto
{
    public class RegistrationShortDataDto
    {
        public int FirmId { get; set; }
        public bool IsOoo { get; set; }
        public bool IsEmployerMode { get; set; }
        public string Pseudonym { get; set; }
        public string ShortPseudonym { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
    }
}