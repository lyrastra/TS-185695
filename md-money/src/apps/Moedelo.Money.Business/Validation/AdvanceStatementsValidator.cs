using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.AdvanceStatements;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Validation
{
    [InjectAsSingleton(typeof(AdvanceStatementsValidator))]
    internal sealed class AdvanceStatementsValidator
    {
        private readonly AdvanceStatementReader advanceStatementReader;

        public AdvanceStatementsValidator(AdvanceStatementReader advanceStatementReader)
        {
            this.advanceStatementReader = advanceStatementReader;
        }

        public async Task ValidateAsync(long documentBaseId, long employeeId)
        {
            var advanceStatement = await advanceStatementReader.GetByBaseIdAsync(documentBaseId);
            if (advanceStatement == null || advanceStatement.DocumentBaseId == 0)
            {
                throw new BusinessValidationException("AdvanceStatements.DocumentBaseId", $"Не найден авансовый отчет с ид {documentBaseId}");
            }
            if (advanceStatement.EmployeeId != employeeId)
            {
                throw new BusinessValidationException("AdvanceStatements.DocumentBaseId", $"Сотрудник в авансовом отчете с ид {documentBaseId} отличается от сотрудника в п/п");
            }
        }

        public async Task ValidateAsync(IReadOnlyCollection<long> documentBaseIds, long employeeId)
        {
            var advanceStatements = await advanceStatementReader.GetByBaseIdsAsync(documentBaseIds);
            var advanceStatementsByBaseId = advanceStatements.ToDictionary(x => x.DocumentBaseId);

            for (var i = 0; i < documentBaseIds.Count; i++)
            {
                var documentBaseId = documentBaseIds.ElementAt(i);

                advanceStatementsByBaseId.TryGetValue(documentBaseId, out var advanceStatement);
                if (advanceStatement == null || advanceStatement.DocumentBaseId == 0)
                {
                    throw new BusinessValidationException($"AdvanceStatements[{i}].DocumentBaseId", $"Не найден авансовый отчет с ид {documentBaseId}");
                }
                if (advanceStatement.EmployeeId != employeeId)
                {
                    throw new BusinessValidationException($"AdvanceStatements[{i}].DocumentBaseId", $"Сотрудник в авансовом отчете с ид {documentBaseId} отличается от сотрудника в п/п");
                }
            }
        }
    }
}
