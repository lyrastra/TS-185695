using AutoFixture;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Access;
using Moedelo.Common.Enums.Enums.WhiteLabels;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.CommonV2.WhiteLabel.Services;
using Moedelo.CommonV2.WhiteLabel.Tests.Extensions;
using Moq;
using NUnit.Framework;

namespace Moedelo.CommonV2.WhiteLabel.Tests.Services;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class WhiteLabelGetterTests
{
    private readonly Fixture randomizer = new Fixture();

    [Test]
    public void WhiteLabelGetter_ShouldReturnTrue_IfWLTariffRulePassed()
    {
        // arrange
        var userInFirmReader = new Mock<IUserInFirmAccessRulesReader>();
        var getter = new WhiteLabelGetter(userInFirmReader.Object);
        // add 5 randomRules
        var rules = randomizer
            .GenerateEnumValuesUseExclusions(5, AccessRule.WlTariff)
            .ToList();
        rules.Add(AccessRule.WlTariff);

        // act 
        var result = getter.IsWhiteLabel(rules);

        // assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void WhiteLabelGetter_ShouldReturnFalse_IfNonWlTariffRulesPassed()
    {
        // arrange
        var userInFirmReader = new Mock<IUserInFirmAccessRulesReader>();
        var getter = new WhiteLabelGetter(userInFirmReader.Object);
        var rules = new List<AccessRule>();
        // add 5 randomRules
        for (int i = 0; i < 5; i++)
        {
            rules.AddRange(randomizer.GenerateEnumValuesUseExclusions(5, AccessRule.WlTariff));
        }

        // act 
        var result = getter.IsWhiteLabel(rules);

        // assert
        Assert.That(result, Is.False);
    }

    [Test]
    [TestCase(WhiteLabelType.Sberbank, AccessRule.SberbankWLSubscriptionAny, AccessRule.SberbankTariff)]
    [TestCase(WhiteLabelType.DeloBank, AccessRule.SkbBankWlTariff)]
    [TestCase(WhiteLabelType.Rncb, AccessRule.RnkbTariff)]
    [TestCase(WhiteLabelType.ProstoBank, AccessRule.ProstoBankTariff)]
    [TestCase(WhiteLabelType.AkbarsBank, AccessRule.AkbarsBankTariff)]
    public async Task WhiteLabelGetter_ShouldReturnProperWl_IfUserHasProperRules(params object[] list)
    {
        // arrange
        var expectedType = (WhiteLabelType) list[0];
        var firmId = randomizer.Create<int>();
        var userId = randomizer.Create<int>();
        var rules = new HashSet<AccessRule>( );
        for(var i = 1; i < list.Length; i++) 
        { 
            rules.Add((AccessRule) list[i]);
        }

        var userInFirmAccessRulesReader = new Mock<IUserInFirmAccessRulesReader>();
        userInFirmAccessRulesReader.Setup(x => x.GetAsync(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(rules);

        var getter = new WhiteLabelGetter(userInFirmAccessRulesReader.Object);

        // act 
        var result = await getter.GetAsync(firmId, userId).ConfigureAwait(false);

        // assert
        Assert.That(result, Is.EqualTo(expectedType));
    }

    [Test]
    public async Task WhiteLabelGetter_ShouldReturnDefaultWlType_IfUserDoesntHaveProperRules()
    {
        // arrange
        var expectedType = WhiteLabelType.Default;
        var firmId = randomizer.Create<int>();
        var userId = randomizer.Create<int>();
        var exclusions = WhiteLabelMapping.PermissionToWhiteLabelType.Keys.ToArray();
        var rules = new HashSet<AccessRule>();
        foreach (var randomRule in randomizer.GenerateEnumValuesUseExclusions(100, exclusions))
        {
            rules.Add(randomRule);
        }

        var userInFirmAccessRulesReader = new Mock<IUserInFirmAccessRulesReader>();
        userInFirmAccessRulesReader
            .Setup(x => x.GetAsync(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(rules);

        var getter = new WhiteLabelGetter(userInFirmAccessRulesReader.Object);

        // act 
        var result = await getter.GetAsync(firmId, userId).ConfigureAwait(false);

        // assert
        Assert.That(result, Is.EqualTo(expectedType));
    }
}