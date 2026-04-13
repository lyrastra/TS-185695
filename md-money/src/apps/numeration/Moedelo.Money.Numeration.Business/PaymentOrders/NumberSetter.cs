using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Numeration.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Numeration.DataAccess.Abstractions.PaymentOrders;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Common.AccessRules.Abstractions;

namespace Moedelo.Money.Numeration.Business.PaymentOrders
{
    [InjectAsSingleton(typeof(INumberSetter))]
    public class NumberSetter : INumberSetter
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IPaymentOrderNumberDao dao;
        private readonly INumberGetter numberGetter;

        public NumberSetter(
            IExecutionInfoContextAccessor contextAccessor,
            IPaymentOrderNumberDao dao,
            INumberGetter numberGetter)
        {
            this.contextAccessor = contextAccessor;
            this.dao = dao;
            this.numberGetter = numberGetter;
        }

        public async Task Last(int settlementAccountId, int year, int numberToSet, bool? isSaveNumeration = null)
        {
            if (!HasAccessToBank())
            {
                return;
            }

            var result = await numberGetter.LastAndNext(settlementAccountId, year);
            var lastNumber = result.Item1;
            var nextNumber = result.Item2.FirstOrDefault();

            //Проверяем, поменял ли пользователь предложенный номер при создании п/п. Не сохраняем в счетчик, если поменял.
            if (numberToSet <= lastNumber || 
               (!isSaveNumeration.GetValueOrDefault() && numberToSet != nextNumber - 1 && numberToSet != lastNumber + 1))
            {
                return;
            }
            var firmId = contextAccessor.ExecutionInfoContext.FirmId;
            await dao.SetLast((int)firmId, settlementAccountId, year, numberToSet);
        }

        private bool HasAccessToBank()
        {
            var rules = new[] { AccessRule.UsnAccountantTariff, AccessRule.AccessToViewAccountingBank };
            var context = contextAccessor.ExecutionInfoContext;
            return context.HasAllRules(rules);
        }
    }
}
