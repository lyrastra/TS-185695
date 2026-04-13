using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.AuditModule.Models;
using Moedelo.InfrastructureV2.Domain.Interfaces.DistributedFileSystem;
using Moedelo.InfrastructureV2.Domain.Models.MongoFiles;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.InfrastructureV2.MongoDataAccess.Extensions;
using Moedelo.InfrastructureV2.MongoDataAccess.Internal;
using Moedelo.InfrastructureV2.MongoDataAccess.Pools.Models;

namespace Moedelo.InfrastructureV2.MongoDataAccess
{
    public abstract class MongoFileStorageBase : IFileStorageBase
    {
        private readonly IInternalMongoFileStorage mongoFileStorage;

        private readonly SettingValue mongoConnectionStringSetting;
        private readonly SettingValue mongoDatabaseNameSetting;
        private readonly SettingValue mongoProductionModeSetting;

        private readonly IAuditTracer auditTracer;

        protected MongoFileStorageBase(
            IInternalMongoFileStorage mongoFileStorage,
            SettingValue mongoConnectionStringSetting,
            SettingValue mongoDatabaseNameSetting,
            SettingValue mongoProductionModeSetting,
            IAuditTracer auditTracer)
        {
            this.mongoFileStorage = mongoFileStorage;
            this.mongoConnectionStringSetting = mongoConnectionStringSetting;
            this.mongoDatabaseNameSetting = mongoDatabaseNameSetting;
            this.mongoProductionModeSetting = mongoProductionModeSetting;
            this.auditTracer = auditTracer;
        }

        public async Task<byte[]> GetFileDataByIdAsync(
            string id,
            CancellationToken cancellationToken,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            var cnn = GetConnection();

            using (var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber)
                       .WithConnection(cnn)
                       .WithParams(new { Id = id })
                       .Start())
            {
                try
                {
                    var result = await mongoFileStorage
                        .GetFileDataByIdAsync(cnn, id, cancellationToken)
                        .ConfigureAwait(false);
                    scope.Span.AddTag("FileSize", result?.Length);

                    return result;
                }
                catch (Exception ex)
                {
                    scope.Span.SetError(ex);
                    throw;
                }
            }
        }

        public async Task<Stream> GetFileDataStreamByIdAsync(
            string id,
            CancellationToken cancellationToken,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            var cnn = GetConnection();

            using (var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber)
                       .WithConnection(cnn)
                       .WithParams(new { Id = id })
                       .Start())
            {
                try
                {
                    return await mongoFileStorage
                        .GetFileDataStreamByIdAsync(cnn, id, cancellationToken)
                        .ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    scope.Span.SetError(ex);
                    throw;
                }
            }
        }

        public async Task<byte[]> GetFileDataByNameAsync(string fileName,
            CancellationToken cancellationToken,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            var cnn = GetConnection();

            using (var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber)
                       .WithConnection(cnn)
                       .WithParams(new { FileName = fileName })
                       .Start())
            {
                try
                {
                    var result = await mongoFileStorage
                        .GetFileDataByFileNameAsync(cnn, PrepareFileName(fileName), cancellationToken)
                        .ConfigureAwait(false);

                    return result;
                }
                catch (Exception ex)
                {
                    scope.Span.SetError(ex);
                    throw;
                }
            }
        }

        public async Task<MongoFileInfo> GetFileNameByIdAsync(string id,
            CancellationToken cancellationToken,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            var cnn = GetConnection();

            using (var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber)
                       .WithConnection(cnn)
                       .WithParams(new { Id = id })
                       .Start())
            {
                try
                {
                    var result = await mongoFileStorage
                        .GetFileNameByIdAsync(cnn, id, cancellationToken)
                        .ConfigureAwait(false);

                    return result;
                }
                catch (Exception ex)
                {
                    scope.Span.SetError(ex);
                    throw;
                }
            }
        }

        public async Task<MongoFileInfoTable> GetFileNamesByPathPatternAsync(string pathPattern,
            int take, int skip, CancellationToken cancellationToken,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            var cnn = GetConnection();

            using (var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber)
                       .WithConnection(cnn)
                       .WithParams(new { PathPattern = pathPattern, Skip = skip, Take = take })
                       .Start())
            {
                try
                {
                    var path = PreparePathPattern(pathPattern);

                    var result = await mongoFileStorage
                        .GetFileNamesByPathPatternAsync(cnn, path, skip, take, cancellationToken)
                        .ConfigureAwait(false);

                    return result;
                }
                catch (Exception ex)
                {
                    scope.Span.SetError(ex);
                    throw;
                }
            }
        }

        public async Task<MongoFileInfoTable> GetAllFileNamesByPathPatternAsync(string pathPattern,
            CancellationToken cancellationToken,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            var cnn = GetConnection();

            using (var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber)
                       .WithConnection(cnn)
                       .WithParams(new { PathPattern = pathPattern })
                       .Start())
            {
                try
                {
                    var path = PreparePathPattern(pathPattern);

                    var response = await mongoFileStorage
                        .GetFileNamesByPathPatternAsync(cnn, path, cancellationToken: cancellationToken)
                        .ConfigureAwait(false);

                    scope.Span.AddTag("PreparedPathPattern", path);
                    scope.Span.AddTag("Response", response);

                    return response;
                }
                catch (Exception ex)
                {
                    scope.Span.SetError(ex);
                    throw;
                }
            }
        }

        public async Task<MongoFileInfoTable> GetFileNamesByPathAsync(
            string path,
            int take = 5,
            int skip = 0,
            CancellationToken cancellationToken = default,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            var cnn = GetConnection();

            using (var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber)
                       .WithConnection(cnn)
                       .WithParams(new { Path = path, Skip = skip, Take = take })
                       .Start())
            {
                try
                {
                    var response = await mongoFileStorage
                        .GetFileNamesByPathAsync(cnn, PrepareFileName(path), skip, take, cancellationToken)
                        .ConfigureAwait(false);

                    scope.Span.AddTag("Response", response);

                    return response;
                }
                catch (Exception ex)
                {
                    scope.Span.SetError(ex);
                    throw;
                }
            }
        }

        public async Task<MongoFileInfoTable> GetAllFileNamesByPathAsync(
            string path,
            CancellationToken cancellationToken,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            var cnn = GetConnection();

            using (var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber)
                       .WithConnection(cnn)
                       .WithParams(new { Path = path })
                       .Start())
            {
                try
                {
                    var response = await mongoFileStorage
                        .GetFileNamesByPathAsync(
                            cnn,
                            PrepareFileName(path),
                            cancellationToken: cancellationToken)
                        .ConfigureAwait(false);

                    scope.Span.AddTag("Response", response);

                    return response;
                }
                catch (Exception ex)
                {
                    scope.Span.SetError(ex);
                    throw;
                }
            }
        }

        public async Task<bool> HasDataByIdAsync(string id,
            CancellationToken cancellationToken,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            var cnn = GetConnection();

            using (var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber)
                       .WithConnection(cnn)
                       .WithParams(new { Id = id })
                       .Start())
            {
                try
                {
                    var result = await mongoFileStorage
                        .HasDataByIdAsync(cnn, id, cancellationToken)
                        .ConfigureAwait(false);

                    return result;
                }
                catch (Exception ex)
                {
                    scope.Span.SetError(ex);
                    throw;
                }
            }
        }

        public async Task<string> SaveFileAsync(string fileName,
            byte[] fileData,
            CancellationToken cancellationToken,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            var cnn = GetConnection();

            using (var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber)
                       .WithConnection(cnn)
                       .WithParams(new { FileName = fileName })
                       .Start())
            {
                try
                {
                    return await mongoFileStorage
                        .SaveFileAsync(cnn, PrepareFileName(fileName), fileData, cancellationToken)
                        .ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    scope.Span.SetError(ex);
                    throw;
                }
            }
        }

        public async Task DeleteFileByIdAsync(
            string id,
            CancellationToken cancellationToken,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            var cnn = GetConnection();

            using (var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber)
                       .WithConnection(cnn)
                       .WithParams(new { Id = id })
                       .Start())
            {
                try
                {
                    await mongoFileStorage
                        .DeleteFileByIdAsync(cnn, id, cancellationToken)
                        .ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    scope.Span.SetError(ex);
                    throw;
                }
            }
        }

        private MongoConnection GetConnection()
        {
            return MongoConnectionHelper.GetMongoConnection(
                mongoConnectionStringSetting,
                mongoDatabaseNameSetting);
        }

        private string PrepareFileName(string fileName)
        {
            var mongoProductionMode = mongoProductionModeSetting.GetBoolValueOrDefault(false);

            if (mongoProductionMode)
            {
                return fileName;
            }

            var filePathPrefix = $"{Environment.MachineName}_";

            return $"{filePathPrefix}{fileName}";
        }

        private string PreparePathPattern(string pathPattern)
        {
            var mongoProductionMode = mongoProductionModeSetting.GetBoolValueOrDefault(false);

            if (mongoProductionMode)
            {
                return pathPattern;
            }

            var pathPatternPrefix = $"{Environment.MachineName}_";

            if (pathPattern.StartsWith("/^"))
            {
                return pathPattern.Insert(2, pathPatternPrefix);
            }

            if (pathPattern.StartsWith("/"))
            {
                return pathPattern.Insert(1, $"^{pathPatternPrefix}");
            }

            return pathPattern.Insert(0, $"^{pathPatternPrefix}");
        }

        private IAuditSpanBuilder GetAuditSpanBuilder(string memberName, string sourceFilePath, int sourceLineNumber)
        {
            var spanName = $"func {memberName} from {sourceFilePath} file at {sourceLineNumber} line";
            var utcNow = DateTimeOffset.UtcNow;
            return auditTracer.BuildSpan(AuditSpanType.MongoDbQuery, spanName).WithStartDateUtc(utcNow);
        }
    }
}
