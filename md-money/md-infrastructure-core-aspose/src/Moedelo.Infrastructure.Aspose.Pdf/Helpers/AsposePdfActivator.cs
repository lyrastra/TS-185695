
using Aspose.Pdf;

namespace Moedelo.Infrastructure.Aspose.Pdf.Helpers
{
    public static class AsposePdfActivator
    {
        private static readonly object LockObject = new object();
        private static volatile bool isInit = false;

        /// <summary>
        /// Активирует лицензию на Aspose.Words (достаточно вызвать 1 раз на приложение) 
        /// </summary>
        public static void ApplyLicense()
        {
            if (isInit)
            {
                return;
            }

            lock (LockObject)
            {
                if (isInit)
                {
                    return;
                }

                isInit = true;

                var assembly = typeof(AsposePdfActivator).Assembly;
                var assemblyName = assembly.GetName().Name;
                const string licenseName = "Aspose.PDF.NET.lic";

                using var stream = assembly.GetManifestResourceStream($"{assemblyName}.{licenseName}");
                if (stream == null)
                {
                    return;
                }
                    
                var license = new License();
                license.SetLicense(stream);
            }
        }
    }
}