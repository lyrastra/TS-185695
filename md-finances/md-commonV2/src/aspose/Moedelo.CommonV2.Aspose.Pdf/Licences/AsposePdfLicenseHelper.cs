namespace Moedelo.CommonV2.ASpose.Pdf.Licences
{
    public static class AsposePdfLicenseHelper
    {
        public static void SetPdfLicense()
        {
            new Aspose.Pdf.License().SetLicense(@"Licences\Aspose.Pdf.lic");
        }
    }
}
