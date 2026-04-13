namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationMonitor
{
    public class StateFakeRequestMovementDto
    {
        /// <summary> Статус ответа на фэйковый запрос
        public string Status;
        /// <summary> Считать ли этот статус ошибкой
        public bool IsError;
    }
}
