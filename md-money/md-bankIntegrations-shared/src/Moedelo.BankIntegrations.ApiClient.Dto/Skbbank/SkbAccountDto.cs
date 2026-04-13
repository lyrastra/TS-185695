namespace Moedelo.BankIntegrations.ApiClient.Dto.Skbbank
{
    public class SkbAccountDto
    {
        /// <summary> Номер р/сч </summary>
        public string Number { get; set; }

        public string Bik { get; set; }

        /// <summary> Наименование р/сч (пользователь сам указывает) </summary>
        public string Name { get; set; }
    }
}