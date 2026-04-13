namespace Moedelo.Accounts.ApiClient.Enums
{
    public enum AccountType
    {
        // для внутренних нужд
        Maintenance = 0,
        // обычный аккаунт (пользовательский)
        ClientCompany = 1,
        // аккаунт для аутсорсеров
        ProfOutsource = 2
    }
}