using System.Threading.Tasks;
using Moedelo.SystemNotifications.Dto;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.SystemNotifications.Client
{
    /// <summary> Клиент для оповещений </summary>
    public interface ISystemNotificationsClient : IDI
    {
        /// <summary>
        /// Получить уведомление по Id
        /// </summary>
        /// <param name="firmId">идентификатор фирмы оператора, осуществляющего запрос (UserContext.FirmId)</param>
        /// <param name="userId">идентификатор пользователя оператора, осуществляющего запрос (UserContext.UserId)</param>
        /// <param name="id">идентификатор уведомления</param>
        /// <returns></returns>
        Task<SystemNotificationEditDto> GetByIdAsync(int firmId, int userId, int id);

        /// <summary>
        /// Получить список уведомлениий по фильтру
        /// </summary>
        /// <param name="firmId">идентификатор фирмы оператора, осуществляющего запрос (UserContext.FirmId)</param>
        /// <param name="userId">идентификатор пользователя оператора, осуществляющего запрос (UserContext.UserId)</param>
        /// <param name="dto">правила фильтрации</param>
        /// <returns>Список уведомлений</returns>
        Task<QueryListResult<SystemNotificationDto>> GetByCriteriaAsync(int firmId, int userId, NotificationsQueryDto dto);

        /// <summary>
        /// Создать новое уведомление
        /// </summary>
        /// <param name="firmId">идентификатор фирмы оператора, осуществляющего запрос (UserContext.FirmId)</param>
        /// <param name="userId">идентификатор пользователя оператора, осуществляющего запрос (UserContext.UserId)</param> 
        /// <param name="notification">Уведомление</param>
        /// <returns>Id оповещения</returns>
        Task<int> CreateNotificationAsync(int firmId, int userId, SystemNotificationEditDto notification);      
        
        /// <summary>
        /// Обновить уведомление
        /// </summary>
        /// <param name="firmId">идентификатор фирмы оператора, осуществляющего запрос (UserContext.FirmId)</param>
        /// <param name="userId">идентификатор пользователя оператора, осуществляющего запрос (UserContext.UserId)</param>
        /// <param name="notification">Уведомление</param>
        /// <returns>Id оповещения</returns>
        Task UpdateNotificationAsync(int firmId, int userId, SystemNotificationEditDto notification);
    }
}
