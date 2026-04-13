using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.AgencyContract;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Business.PaymentOrders.Outsource;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.AgencyContract;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.AgencyContract;

[InjectAsSingleton(typeof(IAgencyContractOutsourceProcessor))]
internal sealed class AgencyContractOutsourceProcessor : PaymentOrderOutsourceProcessor<AgencyContractSaveRequest>, IAgencyContractOutsourceProcessor
{
    private readonly IAgencyContractValidator validator;
    private readonly IAgencyContractReader reader;
    private readonly IAgencyContractUpdater updater;

    public AgencyContractOutsourceProcessor(
        IAgencyContractValidator validator,
        IAgencyContractReader reader,
        IAgencyContractUpdater updater,
        ILogger<AgencyContractOutsourceProcessor> logger) 
        : base(logger)
    {
        this.validator = validator;
        this.reader = reader;
        this.updater = updater;
    }

    public new Task<OutsourceConfirmResult> ConfirmAsync(AgencyContractSaveRequest request)
    {
        return base.ConfirmAsync(request);
    }

    protected override async Task<AgencyContractSaveRequest> MapToExistentAsync(AgencyContractSaveRequest request)
    {
        var existent = await reader.GetByBaseIdAsync(request.DocumentBaseId);
        return MapToExistent(existent, request);
    }

    private static AgencyContractSaveRequest MapToExistent(
        AgencyContractResponse existent,
        AgencyContractSaveRequest newValues)
    {
        var result = AgencyContractMapper.MapToSaveRequest(existent);

        result.OperationState = newValues.OperationState;
        result.OutsourceState = newValues.OutsourceState;
        result.Kontragent = newValues.Kontragent;

        // поменялся контрагент: нужно установить связанные поля в значения по умолчанию
        if (existent.Kontragent?.Id != newValues.Kontragent?.Id)
        {
            result.ContractBaseId = 0; // Падаем в красную таблицу
        }

        return result;
    }

    protected override Task UpdateAsync(AgencyContractSaveRequest request) => updater.UpdateAsync(request);

    protected override Task ValidateAsync(AgencyContractSaveRequest request) => validator.ValidateAsync(request);
}