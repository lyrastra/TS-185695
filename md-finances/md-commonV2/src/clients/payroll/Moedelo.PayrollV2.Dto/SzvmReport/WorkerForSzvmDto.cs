
namespace Moedelo.PayrollV2.Dto.SzvmReport
{
    public class WorkerForSzvmDto
    {
        public int Id { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public string Inn { get; set; }

        public string Snils { get; set; }

        /// <summary>
        /// Полное ФИО сотрудника
        /// </summary>
        public virtual string FullName { get; set; }

        /// <summary>
        /// Фамилия И.О. сотрудника
        /// </summary>
        public virtual string ShortName { get; set; }

        public bool IsForeigner { get; set; }
    }
}