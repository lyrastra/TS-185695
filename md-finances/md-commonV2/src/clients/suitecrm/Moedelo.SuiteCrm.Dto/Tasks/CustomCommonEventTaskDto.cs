namespace Moedelo.SuiteCrm.Dto.Tasks
{
    /// <summary>
    /// Создание задачи в CRM на основе клика по кастомному событию в налоговом календаре
    /// </summary>
    public class CustomCommonEventTaskDto
    {
        public int FirmId { get; set; }

        public string Description { get; set; }
    }
}