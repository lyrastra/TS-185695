using Moedelo.AstralV3.Client.AstralTransferService;
using Moedelo.AstralV3.Client.Types;
using System.Collections.Generic;
using System.Linq;
using FileWithID = Moedelo.AstralV3.Client.BankArchiveService.FileWithID;

namespace Moedelo.AstralV3.Client.Helpers
{
    /// <summary>
    /// Класс с мапперами для астрального клиента.
    /// </summary>
    public static class AstralMapper
    {
        public static FlcResult MapCheckFilesResponseToFlcResult(AstralFlcServiceReference.CheckFilesResponse response)
        {
            return new FlcResult
            {
                Status = response.Result.status,
                Protocol = response.Result.protocol
            };
        }

        /// <summary>Мапает GetNewUIDsResponse в UidWithListReceivers[]</summary>
        /// <param name="answer">Ответ Астарала</param>
        /// <returns>Список уидов со списком получателей</returns>
        public static UidWithListReceivers[] MapGetNewUIDsResponseToUidWithListReceivers(GetNewUIDsResponse answer)
        {
            return answer.UIDsWithSubjects
                .Select(i => new UidWithListReceivers
                    {
                        Uid = i.uid,
                        Receivers = i.receivers,
                        Sender = i.sender
                    })
                .ToArray();
        }

        /// <summary>Получение списка файлов из ответа Астрала</summary>
        /// <param name="answer">Ответ Астрала</param>
        /// <returns>Список файлов</returns>
        public static FileObject[] MapGetFilesByUIDResponseToFileObjects(GetFilesByUIDResponse answer)
        {
            return answer.FilesWithIDs
                .Select(f => new FileObject
                {
                    Name = f.filename,
                    Content = f.content,
                    Identity = f.id,
                    Signatures = f.signatures
                })
                .ToArray();
        }

        public static AstralTransferService.File MapFile(FileObject file)
        {
            return new AstralTransferService.File
            {
                filename = file.Name,
                content = file.Content
            };
        }

        public static AstralTransferService.File[] MapFiles(List<FileObject> files)
        {
            return files
                .Select(MapFile)
                .ToArray();
        }

        public static FileObject MapGetFilesWithUidResponse(FileWithID[] files)
        {
            var file = files.First();
            return new FileObject
            {
                Name = file.filename,
                Content = file.content
            };
        }
    }
}
