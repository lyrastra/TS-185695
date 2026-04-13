using Aspose.Words;

namespace Moedelo.CommonV2.Reports.Aspose.Words
{
    public static class AsposeWordsLicenseInitializer
    {
        public static void Initalize()
        {
            var license = new License();
            if (!license.IsLicensed)
            {
                license.SetLicense("Aspose.Words.lic");
            }
        }
    }
}