using Moedelo.Common.Enums.Enums.ElectronicReports;

namespace Moedelo.Common.Enums.Enums.EdsWizard
{
    /// <summary>
    /// Если меняется название текста - не забыть прогнать миграцию для таблицы [MongoFile]
    /// </summary>
    public static class EdsWizardFileNames
    {
        public const string ConsentOfPersonalData = "Согласие на передачу персональных данных с подписью клиента";

        public const string PassportMainPage = "Второй главный разворот паспорта (с фотографией)";
        
        public const string SnilsMainPage = "Скан-копия СНИЛСа";

        public const string ClientPhotoWithPassport = "Фото клиента с паспортом";

        public const string Statement = "Заявление на выпуск ЭП";

        public const string PassportRegistrationPage = "Страница с информацией о прописке";

        public const string SignedCertificate = "Подписанный сертификат";

        public static int? GetIdForFileName(string fileName)
        {
            switch (fileName)
            {
                case ConsentOfPersonalData: return 1;
                case PassportMainPage: return 2;
                case ClientPhotoWithPassport: return 3;
                case Statement: return 4;
                case PassportRegistrationPage: return 5;
                case SignedCertificate: return 6;
                case SnilsMainPage: return 7;
                default: return null;
            }
        }

        public static string GetFileNameForId(int? fileId)
        {
            if (!fileId.HasValue)
                return null;

            switch (fileId)
            {
                case 1: return ConsentOfPersonalData;
                case 2: return PassportMainPage;
                case 3: return ClientPhotoWithPassport;
                case 4: return Statement;
                case 5: return PassportRegistrationPage;
                case 6: return SignedCertificate;
                case 7: return SnilsMainPage;
                default: return null;
            }
        }
        
        public static RequiredDocumentType GetTypeForId(int? fileId)
        {
            if (!fileId.HasValue)
                return RequiredDocumentType.Unknown;

            switch (fileId)
            {
                case 1: return RequiredDocumentType.ConsentOfPersonalData;
                case 2: return RequiredDocumentType.PassportMainPage;
                case 3: return RequiredDocumentType.ClientPhotoWithPassport;
                case 4: return RequiredDocumentType.Statement;
                case 5: return RequiredDocumentType.PassportRegistrationPage;
                case 6: return RequiredDocumentType.SignedCertificate;
                case 7: return RequiredDocumentType.SnilsMainPage;
                default: return RequiredDocumentType.Unknown;
            }
        }
    }
}
