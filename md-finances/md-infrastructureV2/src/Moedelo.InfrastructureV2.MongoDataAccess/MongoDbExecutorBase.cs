using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.AuditModule.Models;
using Moedelo.InfrastructureV2.Domain.Interfaces.Mongo;
using Moedelo.InfrastructureV2.Domain.Models.Mongo;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.InfrastructureV2.MongoDataAccess.Extensions;
using Moedelo.InfrastructureV2.MongoDataAccess.Internal;
using Moedelo.InfrastructureV2.MongoDataAccess.Pools.Models;

namespace Moedelo.InfrastructureV2.MongoDataAccess
{
    public abstract class MongoDbExecutorBase : IMongoDbExecutor
    {
        private readonly IInternalMongoDbExecutor internalMongoDbExecutor;
        
        private readonly SettingValue mongoConnectionStringSetting;
        private readonly SettingValue mongoDatabaseNameSetting;
        private readonly SettingValue mongoCollectionNameSetting;

        private readonly IAuditTracer auditTracer;

        protected MongoDbExecutorBase(
            IInternalMongoDbExecutor internalMongoDbExecutor, 
            SettingValue mongoConnectionStringSetting, 
            SettingValue mongoDatabaseNameSetting, 
            SettingValue mongoCollectionNameSetting, 
            IAuditTracer auditTracer)
        {
            this.internalMongoDbExecutor = internalMongoDbExecutor;
            this.mongoConnectionStringSetting = mongoConnectionStringSetting;
            this.mongoDatabaseNameSetting = mongoDatabaseNameSetting;
            this.mongoCollectionNameSetting = mongoCollectionNameSetting;
            this.auditTracer = auditTracer;
        }

        public async Task<T> FindByIdAsync<T>(
            string id,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0) where T : class, IMongoObject
        {
            var cnn = GetConnection();
            
            using (var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber)
                .WithConnection(cnn)
                .WithParams(new {Id = id})
                .Start())
            {
                try
                {
                    var result = await internalMongoDbExecutor.FindByIdAsync<T>(cnn, id).ConfigureAwait(false);

                    return result;
                }
                catch (Exception ex)
                {
                    scope.Span.SetError(ex);
                    throw;
                }
            }
        }

        public async Task InsertAsync<T>(
            T document,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0) where T : class, IMongoObject
        {
            var cnn = GetConnection();

            using (var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber)
                .WithConnection(cnn)
                .WithParams(new {Document = document})
                .Start())
            {
                try
                {
                    await internalMongoDbExecutor.InsertAsync(cnn, document).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    scope.Span.SetError(ex);
                    throw;
                }
            }
        }

        public async Task UpdateAsync<T>(
            T document,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0) where T : class, IMongoObject
        {
            var cnn = GetConnection();

            using (var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber)
                .WithConnection(cnn)
                .WithParams(new {Document = document})
                .Start())
            {
                try
                {
                    await internalMongoDbExecutor.UpdateAsync(cnn, document).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    scope.Span.SetError(ex);
                    throw;
                }
            }
        }

        public async Task DeleteByIdAsync<T>(
            string id,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0) where T : class, IMongoObject
        {
            var cnn = GetConnection();

            using (var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber)
                .WithConnection(cnn)
                .WithParams(new {Id = id})
                .Start())
            {
                try
                {
                    await internalMongoDbExecutor.DeleteByIdAsync<T>(cnn, id).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    scope.Span.SetError(ex);
                    throw;
                }
            }
        }

        private MongoCollectionConnection GetConnection()
        {
            return MongoConnectionHelper.GetMongoCollectionConnection(
                mongoConnectionStringSetting,
                mongoDatabaseNameSetting, 
                mongoCollectionNameSetting);
        }
        
        private IAuditSpanBuilder GetAuditSpanBuilder(string memberName, string sourceFilePath, int sourceLineNumber)
        {
            var spanName = $"func {memberName} from {sourceFilePath} file at {sourceLineNumber} line";
            var utcNow = DateTimeOffset.UtcNow;
            return auditTracer.BuildSpan(AuditSpanType.MongoDbQuery, spanName).WithStartDateUtc(utcNow);
        }
    }
}