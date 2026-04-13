namespace Moedelo.BPM.CaseDocs.Client.Dtos
{
    /// <summary>
    ///     Контактные данные
    /// </summary>
    public class ContactDto
    {
        /// <summary>
        ///     Почтовый адрес
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     Имя отправителя
        /// </summary>
        public string Name { get; set; }
    }
}