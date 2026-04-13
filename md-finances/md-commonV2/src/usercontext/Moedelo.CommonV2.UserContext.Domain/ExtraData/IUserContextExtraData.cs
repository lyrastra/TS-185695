using System;

namespace Moedelo.CommonV2.UserContext.Domain.ExtraData;

/// <summary>
/// Дополнительные поля для IUserContext
/// (к основным относятся идентификаторы пользователя, фирмы, роли пользователя в фирме и права, см. IAuthorizationContext)
/// </summary>
public interface IUserContextExtraData
{
    /// <summary>
    /// Логин пользователя
    /// </summary>
    string Login { get; }

    /// <summary>
    /// Имя пользователя
    /// </summary>
    string UserName { get; }

    /// <summary>
    /// Название организации
    /// </summary>
    string OrganizationName { get; }

    /// <summary>
    /// Код роли пользователя в организации
    /// </summary>
    string RoleCode { get; }

    /// <summary>
    /// Название роли пользователя в организации
    /// </summary>
    string RoleName { get; }

    /// <summary>
    /// ИНН организации
    /// </summary>
    string Inn { get; }

    /// <summary>
    /// Признак того, что организация - ООО
    /// </summary>
    bool IsOoo { get; }

    /// <summary>
    /// Признак того, что организация - работодатель
    /// </summary>
    bool IsEmployerMode { get; }

    /// <summary>
    /// Дата регистрации фирмы
    /// </summary>
    DateTime? FirmRegistrationDate { get; }

    /// <summary>
    /// Признак того, что пользователь - внутренний (не учитывается в статистике)
    /// </summary>
    bool IsInternal { get; }
}
