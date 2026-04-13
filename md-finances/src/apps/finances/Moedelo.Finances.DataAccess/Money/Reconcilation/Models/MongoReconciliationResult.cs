using Moedelo.Finances.Domain.Models.Money.Reconciliation;
using Moedelo.InfrastructureV2.Domain.Models.Mongo;
using System.Collections.Generic;

namespace Moedelo.Finances.DataAccess.Money.Reconcilation.Models
{
    public class MongoReconciliationResult : IMongoObject
    {
        public string Id { get; set; }

        /// <summary> Есть в сервисе, нет в выписке </summary>
        public List<ReconciliationOperation> ExcessOperations { get; set; }

        /// <summary> Есть в выписке, нет в сервисе </summary>
        public List<ReconciliationOperation> MissingOperations { get; set; }
    }
}