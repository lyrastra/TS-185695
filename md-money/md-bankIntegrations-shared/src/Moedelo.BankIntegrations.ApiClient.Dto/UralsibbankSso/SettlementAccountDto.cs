namespace Moedelo.BankIntegrations.Dto.UralsibbankSso
{
    public class SettlementAccountDto
    {
        /// <summary> Номер р\сч </summary>
        public string SettlementNumber { get; set; }

        public string BankBic { get; set; }

        /// <summary> Наименование р\сч (обычно его заполняет сам клиент вручную в ДБО) </summary>
        public string SettlementName { get; set; }
    }
}
