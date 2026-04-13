namespace Moedelo.Common.Enums.Enums.Auth
{
    /// <summary>Перечисление ролей авторизации</summary>
    public enum LoginRole
    {
        /// <summary>Пользователь</summary>
        User = 1,
        /// <summary>Супер-админ</summary>
        Root = 2,
        /// <summary>Сотрудник службы поддержки с временным паролем</summary>
        TemporaryUser = 3,
        /// <summary>Не удалось определить тип пользователя</summary>
        None = 4
    }
}