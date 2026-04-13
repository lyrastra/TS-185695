using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.SqlDataAccess.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Interfaces;
using Moedelo.Money.PaymentOrders.DataAccess.Extensions;

namespace Moedelo.Money.PaymentOrders.DataAccess.DbExecutors;

[InjectAsSingleton(typeof(IMoedeloDbExecutor))]
internal class MoedeloDbExecutor(
    ISqlDbExecutor sqlDbExecutor,
    ISettingRepository settingRepository,
    IAuditTracer auditTracer)
    : MoedeloSqlDbExecutorBase(sqlDbExecutor, settingRepository.GetConnectionString(), auditTracer), IMoedeloDbExecutor;