namespace Moedelo.Outsource.Dto.ModuleAccess
{
    /// <summary>
    /// Доступ к модулю для аккаунта
    /// </summary>
    public class ModuleAccessDto
    {
        public int AccountId { get; set; }
        public ModuleType Type { get; set; }
    }
}