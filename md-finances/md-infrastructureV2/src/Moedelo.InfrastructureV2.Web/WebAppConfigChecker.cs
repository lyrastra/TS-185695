using System.Collections.Generic;
using System.Linq;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moedelo.InfrastructureV2.Domain.Interfaces.Web;

namespace Moedelo.InfrastructureV2.Web;

[InjectAsSingleton(typeof(IWebAppConfigChecker))]
internal sealed class WebAppConfigChecker : IWebAppConfigChecker
{
    private const string Tag = nameof(WebAppConfigChecker);
    private readonly IWebAppConfigCheck[] checks;
    private readonly ILogger logger;
    private bool isFirstCheck = true;

    public WebAppConfigChecker(IEnumerable<IWebAppConfigCheck> checks,
        ILogger logger)
    {
        this.checks = checks
            // удаляем дубликаты, поскольку в текущей реализации LightInject возвращает дубликаты в этом списке,
            // поскольку сервисы регистрируются на несколько интерфейсов в иерархии,
            // а библиотека не дедуплицирует список
            .Distinct()
            .ToArray();
        this.logger = logger;
    }

    public void CheckWebAppConfiguration()
    {
        if (isFirstCheck)
        {
            LogChecks();
            isFirstCheck = false;
        }

        foreach(var check in checks)
            check.Check();
    }

    private void LogChecks()
    {
        var types = checks.Select(x => x.GetType().Name).ToArray();
        logger.Info(Tag, $"Web app config checker is running with {types.Length} checks",
            extraData: new { types });
    }
}
