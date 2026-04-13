using Aspose.Cells;

namespace Moedelo.CommonV2.Cells.Helpers
{
    public static class AsposeCellLicenseHelper
    {
        public static void SetLicense()
        {
            var license = new License();
            license.SetLicense("Aspose.Cells.lic");
        }
    }
}