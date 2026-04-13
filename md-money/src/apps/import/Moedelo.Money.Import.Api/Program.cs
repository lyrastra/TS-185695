using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moedelo.Common.AspNet.Mvc.Extensions;
using Moedelo.Common.Audit.Middleware;
using Moedelo.Common.ExecutionContext.Middleware;
using Moedelo.Common.Logging.Configuration;
using Moedelo.Common.Logging.ExtraLog.Audit;
using Moedelo.Common.Logging.ExtraLog.ExecutionContext;
using Moedelo.Common.Logging.ExtraLog.ExtraData;
using Moedelo.Common.Logging.ExtraLog.HttpContext;
using Moedelo.Infrastructure.AspNetCore.Extensions;
using Moedelo.Infrastructure.AspNetCore.Swagger.Extensions;
using Moedelo.Infrastructure.DependencyInjection;
using Moedelo.Money.Import.Api.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .ConfigureMoedeloCommonLogger("Moedelo.Money.Import.Api", builder =>
    {
        builder.AddHttpContextExtraLogFields()
            .AddExecutionInfoContextExtraLogFields()
            .AddAuditInfoContextExtraLogFields()
            .AddExtraDataContextExtraLogFields();
    });

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMoedeloMvc(); // NewtonsoftJson ? CompatibilityVersion ??????????????? ??????
builder.Services.AddVersionedApiExplorer(o => o.GroupNameFormat = "'v'V");
builder.Services.AddApiVersioning(
    options =>
    {
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.ReportApiVersions = true;
    });
builder.Services.AddMoedeloSwagger("Moedelo.Money.Import.Api", "Moedelo Money Import Api");
builder.Services.AddHttpContextAccessor();
builder.Services.RegisterByDIAttribute("Moedelo.*");

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UsePathBase("/MoneyImport");
app.UseMoedeloCors();
app.UsePing();
app.UseMoedeloSwagger();
app.UseExecutionInfoContext();
app.UseAuditApiHandlerTrace();
app.UseRejectionOfUnauthorizedRequests();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

app.Run();