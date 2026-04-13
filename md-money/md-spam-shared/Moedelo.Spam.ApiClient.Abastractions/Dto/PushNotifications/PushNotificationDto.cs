using System;

namespace Moedelo.Spam.ApiClient.Abastractions.Dto.PushNotifications;

public class PushNotificationDto
{
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Идентификатор фирмы
    /// </summary>
    public int FirmId { get; set; }

    /// <summary>
    /// Тип пуша
    /// </summary>
    public string PushType { get; set; }

    /// <summary>
    /// Может ли пуш быть отложен
    /// </summary>
    public bool CanBeDeferred { get; set; }

    /// <summary>
    /// Предпочтительная дата и время отправки push (работает совместно с параметром CanBeDeffered = true)
    /// </summary>
    public DateTime? PreferredSendDate { get; set; }

    /// <summary>
    /// Надо ли слать смс, если пользоватеть не посмотрел пуш в течение какого-то кол-ва времени
    /// </summary>
    public bool IsDeliveryRequired { get; set; }

    /// <summary>
    /// Json с данными
    /// </summary>
    public string Data { get; set; }

    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreateDate { get; set; }

    /// <summary>
    /// Дата отправки пользователю
    /// </summary>
    public DateTime? SendDate { get; set; }

    /// <summary>
    /// Просмотрен ли пользователем
    /// </summary>
    public bool IsViewed { get; set; }
}