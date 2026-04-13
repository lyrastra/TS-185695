namespace Moedelo.Payroll.Kafka.Abstractions.Extensions
{
    public static class WorkerFioExtensions
    {
        /// <summary>
        /// Полное ФИО сотрудника
        /// <see cref="https://gitlab.mdtest.org/development/md-payroll/blob/58ac1e034098d47292adba171cc3e7acec2bef3a/src/apps/payroll/Moedelo.Payroll.Domain/Models/Workers/SalaryWorkerModelExt.cs#L52"/>
        /// </summary>
        public static string GetFullName(this IWorkerFio worker)
        {
            if (worker == null) return string.Empty;

            string fullName = string.IsNullOrWhiteSpace(worker.Surname) ? string.Empty : worker.Surname.Trim();

            if (string.IsNullOrEmpty(worker.Name))
            {
                return fullName;
            }

            fullName = $"{fullName} {worker.Name.Trim()}";

            if (string.IsNullOrEmpty(worker.Patronymic))
            {
                return fullName;
            }

            fullName = $"{fullName} {worker.Patronymic.Trim()}";

            return fullName;
        }

        /// <summary>
        /// Фамилия И.О. сотрудника
        /// <see cref="https://gitlab.mdtest.org/development/md-payroll/blob/58ac1e034098d47292adba171cc3e7acec2bef3a/src/apps/payroll/Moedelo.Payroll.Domain/Models/Workers/SalaryWorkerModelExt.cs#L28"/>
        /// </summary>
        public static string GetShortName(this IWorkerFio worker)
        {
            string fullName = worker.Surname ?? string.Empty;

            if (string.IsNullOrEmpty(worker.Name))
            {
                return fullName;
            }

            fullName = $"{fullName} {worker.Name[0]}.";

            if (string.IsNullOrEmpty(worker.Patronymic))
            {
                return fullName;
            }

            fullName = $"{fullName} {worker.Patronymic[0]}.";

            return fullName;
        }
    }
}