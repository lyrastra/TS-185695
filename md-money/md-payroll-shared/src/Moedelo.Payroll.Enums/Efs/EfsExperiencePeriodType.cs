using System.ComponentModel;

namespace Moedelo.Payroll.Enums.Efs
{
    public enum EfsExperiencePeriodType
    {
        [Description("")]
        None = 0,
        [Description("ДОГОВОР")]
        Contract,
        [Description("НЕОПЛДОГ")]
        NotPaidContract,
        [Description("НЕОПЛ")]
        NotPaid,
        [Description("НЕОПЛ")]
        Truancy,
        [Description("ВРНЕТРУД")]
        SickList,
        [Description("ДЕКРЕТ")]
        Pregnancy,
        [Description("ДЕТИ")]
        ChildCare,
        [Description("ДЛДЕТИ")]
        ChildYoungerThan3Year,
        [Description("ДЕТИПРЛ")]
        ChildCareForNotParents,
        [Description("НЕОПЛ")]
        FounderWithoutContract,
        [Description("ДЕТИПРЛ")]
        ChildCareForNotParentsThan3Year,
        [Description("УЧОТПУСК")]
        StudyVacation
    }
}