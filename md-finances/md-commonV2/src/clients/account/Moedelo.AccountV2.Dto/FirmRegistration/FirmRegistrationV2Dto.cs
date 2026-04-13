using Moedelo.Common.Enums.Enums.Requisites;

namespace Moedelo.AccountV2.Dto.FirmRegistration
{
    public class FirmRegistrationV2Dto
    {
        public int Id { get; set; }

        /// <summary> Guid </summary>
        public string RegistrationGuid { get; set; }

        /// <summary> Подтвержден ли email </summary>
        public bool IsEmailConfirm { get; set; }

        /// <summary> Тип регистрации </summary>
        public FirmRegistrationType RegistrationType { get; set; }

        /// <summary> Это регистрация определённого типа? </summary>
        public bool IsTypeRegistration { get; set; }

        /// <summary> Завершена ли регистрация </summary>
        public bool IsRegistrationOver { get; set; }

        /// <summary> ИД Фирмы </summary>
        public int FirmId { get; set; }

        /// <summary> Если была хотя бы одна успешная оплата </summary>
        public bool PaySuccess { get; set; }

        /// <summary> Страница регистрации </summary>
        public string RegisteredFromPage { get; set; }
    }
}