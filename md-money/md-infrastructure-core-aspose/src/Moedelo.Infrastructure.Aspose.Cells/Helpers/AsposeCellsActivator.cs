using Aspose.Cells;

namespace Moedelo.Infrastructure.Aspose.Cells.Helpers
{
    public static class AsposeCellsActivator
    {
        private static readonly object LockObject = new object();
        private static volatile bool isInit = false;

        /// <summary>
        /// Активирует лицензию на Aspose.Cells (достаточно вызвать 1 раз на приложение) 
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
                
                var assembly = typeof(AsposeCellsActivator).Assembly;
                var assemblyName = assembly.GetName().Name;
                var licenseName = "Aspose.Cells.lic";

                using (var stream = assembly.GetManifestResourceStream($"{assemblyName}.{licenseName}"))
                {
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
}