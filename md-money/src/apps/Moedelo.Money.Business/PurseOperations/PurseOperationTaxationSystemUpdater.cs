using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Events;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PurseOperations;
using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PurseOperations
{
    [InjectAsSingleton(typeof(IPurseOperationTaxationSystemUpdater))]
    internal class PurseOperationTaxationSystemUpdater : IPurseOperationTaxationSystemUpdater
    {
        private readonly ILogger logger;
        private readonly IPurseOperationGetter purseOperationGetter;
        private readonly ITaxationSystemChangedEventWriter eventWriter;
        private readonly Dictionary<OperationType, IConcreteTaxationSystemUpdater> updaters;

        public PurseOperationTaxationSystemUpdater(
            ILogger<PurseOperationTaxationSystemUpdater> logger,
            IPurseOperationGetter purseOperationGetter,
            IEnumerable<IConcreteTaxationSystemUpdater> updaters,
            ITaxationSystemChangedEventWriter eventWriter)
        {
            this.logger = logger;
            this.purseOperationGetter = purseOperationGetter;
            this.eventWriter = eventWriter;
            this.updaters = updaters.Select(x =>
                 new
                 {
                     x.GetType().GetCustomAttribute<OperationTypeAttribute>().OperationType,
                     Updater = x
                 })
                .ToDictionary(x => x.OperationType, x => x.Updater);
        }

        public async Task UpdateAsync(long documentBaseId, TaxationSystemType taxationSystemType, Guid guid)
        {
            try
            {
                var operationType = await purseOperationGetter.GetOperationTypeAsync(documentBaseId);
                if (updaters.TryGetValue(operationType, out var updater) == false)
                {
                    throw new NotImplementedException($"Implementation of IConcreteTaxationSystemUpdater for opration type {operationType} ({(int)operationType}) is not found");
                }
                await updater.UpdateAsync(documentBaseId, taxationSystemType);
            }
            catch (OperationNotFoundException)
            {
                logger.LogWarning($"Unable to change taxation system for purse operation with DocumentBaseId = {documentBaseId}. Operation not found");
            }

            var taxationSystemChangedEvent = new TaxationSystemChangedEvent
            {
                DocumentBaseId = documentBaseId,
                TaxationSystemType = taxationSystemType,
                Guid = guid
            };
            await eventWriter.WriteAsync(taxationSystemChangedEvent);
        }
    }
}
