namespace Moedelo.Common.Enums.Enums.EdsRequestTasks
{
    /// <summary>
    /// Тип задачи в партнерке на запрос ЭП
    /// </summary>
    public enum EdsRequestTaskType
    {
        /// <summary>
        /// Запрос на изменение ЭП
        /// </summary>
        Change = 1, 
        
        /// <summary>
        /// Запрос на продление ЭП
        /// </summary>
        Prolongation = 2,
        
        /// <summary>
        /// Заявка от партнера на изменение ЭП
        /// </summary>
        PartnerChange = 3,
        
        /// <summary>
        /// Заявка от партнера на продление ЭП
        /// </summary>
        PartnerProlong = 4,
        
        /// <summary>
        /// Заявка от партнера на создание ЭП
        /// </summary>
        PartnerCreate = 5
    }
}