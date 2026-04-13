using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Archive;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Interfaces.Business.Payment;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moedelo.PaymentImport.Client.MovementList.Storage;
using Moedelo.PaymentOrderImport.Enums;

namespace Moedelo.Finances.Business.Services.PaymentOrders
{
    [InjectAsSingleton(typeof(IPaymentExportService))]
    internal sealed class PaymentExportService(ILogger logger,
        IMovementListIntegrationStorageClient movementListIntegrationStorageClient, 
        IMovementListUserStorageClient userStorageClient, 
        IMovementListStorageClient movementListStorageClient) : IPaymentExportService
    {
        private const int MaxCountFilesInArchive = 5;
        private const string Tag = nameof(PaymentExportService);
        
        public async Task<byte[]> GetZipFileAsync(IUserContext userContext, Encoding encoding, CancellationToken cancellationToken)
        {
            var fileInfos = await movementListStorageClient
                .GetAsync(userContext.FirmId, MaxCountFilesInArchive, cancellationToken)
                .ConfigureAwait(false);

            if (fileInfos is not { Length: > 0 })
            {
                return [];
            }

            var items = new List<ZipItem>();
            foreach (var fileInfo in fileInfos.OrderByDescending(x => x.UploadDate))
            {
                var fileDataUtf8 = fileInfo.Source switch
                {
                    (int)MovementSource.Integration => await movementListIntegrationStorageClient
                        .GetBytesAsync(fileInfo.Path, cancellationToken)
                        .ConfigureAwait(false),
                    (int)MovementSource.User => await userStorageClient.GetBytesAsync(fileInfo.Path, cancellationToken)
                        .ConfigureAwait(false),
                    _ => null
                };

                if (fileDataUtf8 == null)
                {
                    logger.Info(Tag, $"File with id = {fileInfo.Path} not found.");
                    continue;
                }

                items.Add(new ZipItem(fileInfo.Name.Replace('\\', '_'), fileDataUtf8));
            }

            if (items.Count > 0)
            {
                return await Zip.PackWithUniqueNamesAsync(items, encoding ?? Encoding.GetEncoding(866))
                    .ConfigureAwait(false);
            }

            return [];
        }
    }
}

