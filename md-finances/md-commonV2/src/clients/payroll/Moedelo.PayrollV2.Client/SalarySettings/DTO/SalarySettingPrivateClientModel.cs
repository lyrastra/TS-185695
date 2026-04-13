using System.Collections.Generic;

namespace Moedelo.PayrollV2.Client.SalarySettings.DTO
{
    public class SalarySettingPrivateClientModel
    {
        public SalarySettingPrivateClientModel()
        {
            
        }

        public SalarySettingPrivateClientModel(
            SalarySettingDto salarySetting, 
            List<FirmFactorDto> firmFactors, 
            PilotProjectRegionCodeDto pilotProjectRegionCode,
            SalarySettingPermissionsDto salarySettingPermissions)
        {
            SalarySetting = salarySetting;
            FirmFactors = firmFactors;
            PilotProjectRegionCode = pilotProjectRegionCode;
            SalarySettingPermissions = salarySettingPermissions;
        }

        public SalarySettingDto SalarySetting { get; set; }

        public SalarySettingPermissionsDto SalarySettingPermissions { get; set; }
        
        public List<FirmFactorDto> FirmFactors { get; set; }
        
        public PilotProjectRegionCodeDto PilotProjectRegionCode { get; set; }
    }
}
