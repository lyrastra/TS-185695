using System.ComponentModel;

namespace Moedelo.OutSystemsIntegrationV2.Dto.CaseBookApi.Arbitration
{
    public enum CaseStateEnumDto
    {
        [Description("В производстве")]
        Opened,

        [Description("Возможно обжалование")]
        CanAppeal,
        
        [Description("Возможно восстановление срока обжалования")]
        CanRestoreAppeal,
        
        [Description("Завершено")]
        Finished
    }
}