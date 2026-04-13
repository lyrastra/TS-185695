using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Validation.Extensions;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Validation
{
    [InjectAsSingleton(typeof(PatentValidator))]
    internal sealed class PatentValidator
    {
        private const string Name = "PatentId";

        private readonly IExecutionInfoContextAccessor executionInfoContextAccessor;
        private readonly IPatentApiClient patentApiClient;

        public PatentValidator(
            IExecutionInfoContextAccessor executionInfoContextAccessor,
            IPatentApiClient patentApiClient)
        {
            this.executionInfoContextAccessor = executionInfoContextAccessor;
            this.patentApiClient = patentApiClient;
        }

        public async Task ValidateAsync(long? patentId, string prefix = null)
        {
            if (patentId == null)
            {
                return;
            }

            var context = executionInfoContextAccessor.ExecutionInfoContext;

            try
            {
                // если патент не найден, api возвращает 500, что приводит к возникновению исключения
                var patent = await patentApiClient.GetWithoutAdditionalDataByIdAsync(
                    context.FirmId, context.UserId, patentId.Value);

                if (patent == null || patent.Id != patentId.Value)
                {
                    throw new Exception("Патент не найден");
                }
            }
            catch (Exception)
            {
                throw new BusinessValidationException(Name.WithPrefix(prefix), $"Не найден патент с ид {patentId}");
            }
        }
    }
}