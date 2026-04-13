using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Web;

namespace Moedelo.InfrastructureV2.Injection.Web;

[InjectAsSingleton(typeof(IDIControllersCheckerService), typeof(IWebAppConfigCheck))]
internal sealed class DiControllersCheckerService : IDIControllersCheckerService, IWebAppConfigCheck
{
    private readonly IDIChecks diChecks;
    private bool checkComplete;

    public DiControllersCheckerService(IDIChecks diChecks)
    {
        this.diChecks = diChecks;
        checkComplete = false;
    }

    public void CheckControllersCreation()
    {
        if (!checkComplete)
        {
            lock (diChecks)
            {
                if (!checkComplete)
                {
                    diChecks.CheckControllersCreation();
                    checkComplete = true;
                }
            }
        }
    }

    public void Check() => CheckControllersCreation();
}