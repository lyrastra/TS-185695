using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Kbks;
using Moedelo.Money.Business.Validation.Extensions;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;
using System;
using System.Threading.Tasks;
using Moedelo.Infrastructure.System.Extensions.Enums;

namespace Moedelo.Money.Business.Validation
{
    [InjectAsSingleton(typeof(TradingObjectValidator))]
    internal sealed class TradingObjectValidator
    {
        private const string Name = "TradingObjectId";


        public TradingObjectValidator()
        {

        }

        public void ValidateAsync(Kbk kbk, int? tradingObjectId, string prefix = null)
        {
            if (kbk.AccountCode == Enums.BudgetaryAccountCodes.TradingFees && !tradingObjectId.HasValue)
            {
                throw new BusinessValidationException("TradingObjectId".WithPrefix(prefix), $"Дяя кбк {kbk.AccountCode.GetDescription()} не указан торговый объект");
            }
        }
    }
}