using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountV2.Client.ConsoleUser;
using Moedelo.Finances.Domain.Interfaces.Business.Money.Surf;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;

namespace Moedelo.Finances.CollectorForMachineLearning
{
    [InjectAsSingleton]
    public class Console : IDI
    {
        private const string Tag = nameof(Console);
        private const string Name = "Moedelo.Finances.CollectorForMachineLearning";

        private readonly ILogger logger;
        private readonly IDIResolver diResolver;
        private readonly IConsoleUserApiClient consoleUserApi;
        private readonly IPaymentOrderTo1cDocumentConfirmer documentConfirmer;

        public Console(
            ILogger logger,
            IDIResolver diResolver,
            IConsoleUserApiClient consoleUserApi, 
            IPaymentOrderTo1cDocumentConfirmer documentConfirmer)
        {
            this.logger = logger;
            this.diResolver = diResolver;
            this.consoleUserApi = consoleUserApi;
            this.documentConfirmer = documentConfirmer;
        }

        public async Task RunAsync()
        {
            var consoleUser = await consoleUserApi.GetOrCreateByLoginAsync(Name).ConfigureAwait(false);
            if (consoleUser == null)
            {
                logger.Error(Tag, $"User \"{Name}\" is not found");
                return;
            }

            var fileRecs = new List<string>
            {
                "FirmId;DocumentBaseId;OpType;OpPurpose;KontragentInn;IsProprietaryInn;IsBankInn;RecvAcc;IsProprietaryRecvAcc;SndrAcc;IsProprietarySndrAcc;RecvBank;SndrBank;OpSum;OpDate;Direction;OKVED;SourceFileId"
            };

            using (diResolver.BeginScope())
            {
                var recs = await documentConfirmer.ConfirmAsync(consoleUser.Id).ConfigureAwait(false);
                foreach (var rec in recs)
                {
                    fileRecs.Add(rec.ToStrCvs());
                }

                if (fileRecs.Count > 1)
                {
                    var fileName = "ML_" + DateTime.Now.ToString("ddMMyyyy_hh_mm_ss") + ".cvs";
                    try
                    {
                        FileHelper.Save(".\\" + fileName, fileRecs);
                    }
                    catch (Exception e)
                    {
                        logger.Error(Tag, $"Error save file: \"{fileName}\"  Error msg: " + e.Message);
                        throw;
                    }
                    
                }
            }
        }
    }
}
