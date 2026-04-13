namespace Moedelo.RequisitesV2.Dto.AccountingPolicy
{
    /// <summary>
    /// Результат смены системы налогообложения
    /// </summary>
    public class AccountingPolicyChangeResultDto
    {
        /// <summary>
        /// Признак успешности смены СНО
        /// </summary>
        public bool IsSuccessChange { get; set; }

        /// <summary>
        /// Сообщение о статусе смены СНО
        /// </summary>
        public string Message { get; set; }
    }
}