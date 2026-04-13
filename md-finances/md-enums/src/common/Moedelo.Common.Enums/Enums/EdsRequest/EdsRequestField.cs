using System.ComponentModel;

namespace Moedelo.Common.Enums.Enums.EdsRequest
{
    /// <summary>
    /// Поле заявки на выпуск/перевыпуск/продление ЭЦП
    /// </summary>
    public enum EdsRequestField
    {
        [Description("ИНН")]
        Inn = 0,
        [Description("СНИЛС")]
        Snils = 1,
        [Description("ОГРН")]
        Ogrn = 2,
        [Description("Наименование полное")]
        Name = 3,
        [Description("Наименование сокращенное")]
        ShortName = 4,
        [Description("Имя")]
        FirstName = 5,
        [Description("Отчество")]
        SecondName = 6,
        [Description("Фамилия")]
        LastName = 7,
        [Description("Должность")]
        Vacancy = 8,
        [Description("Адрес")]
        Address = 9,
        [Description("Телефон")]
        MobilePhone = 10,
        [Description("Серия паспорта")]
        PassportSerial = 11,
        [Description("Номер паспорта")]
        PassportNumber = 12,
        [Description("Кем выдан паспорт")]
        PassportDepartment = 13,
        [Description("Код подразделения")]
        PassportDepartmentCode = 14,
        [Description("Дата выдачи паспорта")]
        PassportDate = 15,
        [Description("Пол")]
        Gender = 16,
        [Description("Гражданство")]
        Citizenship = 17,
        [Description("Дата рождения")]
        DateOfBirth = 18,
        [Description("Место рождения")]
        PlaceOfBirth = 19,
        [Description("ФНС")]
        Fns = 20,
        [Description("РОССТАТ")]
        Fsgs = 21,
        [Description("ПФР")]
        Pfr = 22,
        [Description("ФСС")]
        Fss = 23,
        [Description("Рег. номер страхователя ПФР")]
        PfrNumber = 24,
        [Description("Рег. номер страхователя ФСС")]
        FssNumber = 25,
        [Description("КПП")]
        Kpp = 26,
        [Description("Логин")]
        Email = 27,
        [Description("Авторасшифрование")]
        IsFnsAutodecryptionEnabled = 28
    }
}