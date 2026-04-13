using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.SyntheticAccounts;

namespace Moedelo.PayrollV2.Dto.Employees
{
    public class WorkerAccountSettingAndDivisionSubcontoDto
    {
        public int WorkerId { get; set; }

        /// <summary>
        /// Код бух.счета, с которого сотруднику начисляется з\п 
        /// </summary>
        public SyntheticAccountCode AccountCode { get; set; }

        /// <summary>
        /// Для 20 счета: вид деятельности
        /// </summary>
        public NomenclatureGroupCode? NomenclatureGroupCode { get; set; }

        /// <summary>
        /// Id субконто отдела, к которому относится сотрудник
        /// Если у сотрудника нет отдела, то будет субконто "Все отделы"
        /// </summary>
        public long DivisionSubcontoId { get; set; }

        /// <summary>
        /// Название субконто отдела, к которому относится сотрудник.
        /// Если отдела нет, то название будет "Все отделы"
        /// </summary>
        public string DivisionName { get; set; }
    }
}