namespace Moedelo.Outsource.Dto.Roles
{
    public class RolePostDto
    {
        public int AccountId { get; set; }
        
        public string Name { get; set; }

        /// <summary>
        /// Список доступных прав (на момент создания класса):
        /// https://github.com/moedelo/md-outsource-shared/blob/13248397df0517532488c25a00a8d9e423de6563/src/Moedelo.Outsource.Enums/Roles/AccessRule.cs#L5
        /// </summary>
        public string[] Rules { get; set; }
    }
}