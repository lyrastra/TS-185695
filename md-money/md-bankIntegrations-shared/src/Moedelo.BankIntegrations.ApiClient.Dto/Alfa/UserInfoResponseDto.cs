namespace Moedelo.BankIntegrations.ApiClient.Dto.Alfa;

public class UserInfoResponseDto
{
    /// <summary>
    /// Полное имя конечного пользователя в отображаемой форме,
    /// включая все части имени,
    /// возможно, включая заголовки и суффиксы,
    /// упорядоченные в соответствии с локалью и предпочтениями конечного пользователя
    /// </summary>
    public string Name { get; set; } 

    /// <summary>
    /// Имя конечного пользователя
    /// </summary>
    public string GivenName { get; set; } 

    /// <summary>
    /// Фамилия конечного пользователя
    /// </summary>
    public string FamilyName { get; set; } 

    /// <summary>
    /// Отчество конечного пользователя
    /// </summary>
    public string MiddleName { get; set; } 

    /// <summary>
    /// Пол конечного пользователя
    /// </summary>
    public string Gender { get; set; } 
    
    /// <summary>
    /// День рождения конечного пользователя, представленный в формате ISO 8601: 2004 [ISO8601‑2004] ГГГГ-ММ-ДД
    /// </summary>
    public string Birthdate { get; set; }
    
    /// <summary>
    /// Предпочитаемый номер телефона конечного пользователя
    /// </summary>
    public string Phone { get; set; } 

    /// <summary>
    /// Гражданство конечного пользователя (для юридических лиц не заполняется)
    /// </summary>
    public string Citizenship { get; set; } 
    
    /// <summary>
    /// Время последнего обновления информации о конечном пользователе.
    /// </summary>
    public string UpdatedAt { get; set; } 
}