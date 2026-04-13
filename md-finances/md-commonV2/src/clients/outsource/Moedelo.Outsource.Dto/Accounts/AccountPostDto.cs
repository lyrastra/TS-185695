namespace Moedelo.Outsource.Dto.Accounts
{
    public class AccountPostDto
    {
        /// <summary>
        /// Идентификатор аккаунта в ЛК
        /// </summary>
        public int Id { get; set; }
        
        public string Name { get; set; }

        /// <summary>
        /// Программист 1С
        /// </summary>
        public int? Programmer1CEmployeeId { get; set; }
        
        /// <summary>
        /// БА по оформлению клиентов
        /// </summary>
        public int? ClientRegistrationEmployeeId { get; set; }
        
        /// <summary>
        /// БА по восстановлению учета
        /// </summary>
        public int? RecoveryAccountantEmployeeId { get; set; }
        
        ///<summary>
        /// Ответственный по разовым услугам (руководитель группы Разовые)
        /// </summary>
        public int? OneTimeServicesEmployeeId { get; set; }
        
        /// <summary>
        /// Ответственное лицо. Привязывать к нему то, что само не распределилось.
        /// </summary>
        public int ResponsibleEmployeeId { get; set; }
        
        /// <summary>
        /// Домен почты сотрудников. Например, @moedelo.org, @krd.moedelo.org, и т.п.
        /// </summary>
        public string LoginDomain { get; set; }
        
        /// <summary>
        /// Количество сотрудников
        /// </summary>
        public int EmployeeCount { get; set; }
    }
}