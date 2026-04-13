namespace Moedelo.OutSystemsIntegrationV2.Dto.MassDirectorsAndFounders
{
    public class MassDirectorsAndFoundersResponseDto
    {
        public int Id { get; set; }

        public MassDirectorsAndFoundersTypeDto Type { get; set; }

        public string Inn { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public int? UlCount { get; set; }
    }
}
