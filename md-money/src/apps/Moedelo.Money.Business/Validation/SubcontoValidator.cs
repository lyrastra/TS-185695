using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.AccPostings.ApiClient.Abstractions.legacy.Subconto.Dto;
using Moedelo.AccPostings.Enums;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.SyntheticAccounts;
using Moedelo.Money.Business.AccPostings;

namespace Moedelo.Money.Business.Validation;


[InjectAsSingleton(typeof(SubcontoValidator))]
internal class SubcontoValidator
{
    private static readonly IReadOnlyList<int> TextSubcontoAccountCodes = new[]
    {
        (int)SyntheticAccountCode._53_01,
        (int)SyntheticAccountCode._55_03,
        (int)SyntheticAccountCode._55_04,
        (int)SyntheticAccountCode._76_41,
        (int)SyntheticAccountCode._860100
    };
    
    private readonly ISyntheticAccountReader syntheticAccountReader;

    public SubcontoValidator(ISyntheticAccountReader syntheticAccountReader)
    {
        this.syntheticAccountReader = syntheticAccountReader;
    }

    public async Task ValidateAsync(
        string propName,
        int accountCode,
        IReadOnlyCollection<long> subcontoIds,
        IReadOnlyDictionary<long, SubcontoDto> subcontoMap)
    {
        if (!TextSubcontoAccountCodes.Contains(accountCode)
            && subcontoIds.Any(x => x == 0))
        {
            throw new BusinessValidationException(propName, "Не указан идентификатор субконто");
        }

        var notFoundSubcontoIds = subcontoIds.Where(x => x > 0 &&  !subcontoMap.ContainsKey(x)).ToArray();
        if (notFoundSubcontoIds.Length > 0)
        {
            throw new BusinessValidationException(propName, $"Не найдены субконто с ид {string.Join(", ", notFoundSubcontoIds)}");
        }

        if (TextSubcontoAccountCodes.Contains(accountCode))
        {
            return;
        }

        var subcontoRules = await syntheticAccountReader.GetSubcontoRulesAsync((SyntheticAccountCode)accountCode);
        var subcontos = subcontoMap.Join(
                subcontoIds,
                kv => kv.Key,
                id => id,
                (kv, _) => kv.Value)
            .ToArray();

        var missedSubcontoTypes = subcontoRules
            .Where(r => subcontos.All(s => s.Type != r.SubcontoType))
            .Select(r => r.SubcontoType)
            .ToArray();

        if (missedSubcontoTypes.Any())
        {
            throw new BusinessValidationException(propName, $"Не указаны субконто с типами: {string.Join(", ", missedSubcontoTypes)}");
        }

        var excessSubcontoTypes = subcontos
            .Where(s => !subcontoRules.Any(r => r.SubcontoType == s.Type))
            .Select(s => s.Type)
            .ToArray();

        if (excessSubcontoTypes.Any())
        {
            throw new BusinessValidationException(propName, $"Для счета {accountCode} указано субконто с недоступными типами: {string.Join(", ", excessSubcontoTypes)}");
        }
    }
}