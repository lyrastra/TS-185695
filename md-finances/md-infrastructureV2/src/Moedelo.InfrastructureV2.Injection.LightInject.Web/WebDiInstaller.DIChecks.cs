using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Moedelo.InfrastructureV2.Injection.LightInject.Web;

public partial class WebDiInstaller
{
    public void CheckControllersCreation()
    {
        var watch = Stopwatch.StartNew();
        var controllers = GetControllers().ToList();
        var controllerNames = new ConcurrentBag<string>();

        using (BeginScope())
        {
            foreach (var controller in controllers)
            {
                try
                {
                    statefulContainer.GetInstance(controller);
                    controllerNames.Add(controller.FullName);
                }
                catch (ReflectionTypeLoadException e)
                {
                    logger.Error(
                        tag,
                        $"При создании контроллера {controller.FullName} произошла ошибка типа {e.GetType().Name}",
                        e,
                        extraData: new
                        {
                            LoaderExceptionsMessages = e.LoaderExceptions.Select(le => le.Message).ToArray()
                        });
                    throw;
                }
                catch (Exception e)
                {
                    var regex = new Regex(@".*Requested dependency: ServiceType:(.*),.*");
                    var match = regex.Match(e.InnerException?.Message ?? string.Empty);
                    if (match.Success)
                    {
                        logger.Error(tag,
                            $"При создании контроллера {controller.FullName} произошла ошибка внедрения зависимостей",
                            e, extraData: new { UnresolvedDependency = match.Groups[1].ToString() });
                    }
                    else
                    {
                        logger.Error(tag, $"При создании контроллера {controller.FullName} произошла ошибка", e);
                    }

                    throw;
                }
            }
        }

        watch.Stop();

        logger.Info(tag, $"Done controllers creation check ({controllers.Count} controllers) at {watch.Elapsed:g}",
            extraData: controllerNames.OrderBy(name => name).ToArray());
    }

    public void EnsureServicesCanBeCreated(IEnumerable<Type> services)
    {
        var serviceNames = new List<string>();

        using (BeginScope())
        {
            foreach (var serviceType in services)
            {
                try
                {
                    statefulContainer.GetInstance(serviceType);
                    serviceNames.Add(serviceType.FullName);
                }
                catch (ReflectionTypeLoadException e)
                {
                    logger.Error(
                        tag,
                        $"При создании сервиса {serviceType.FullName} произошла ошибка типа {e.GetType().Name}",
                        e,
                        extraData: new
                        {
                            LoaderExceptionsMessages = e.LoaderExceptions.Select(le => le.Message).ToArray()
                        });
                    throw;
                }
                catch (Exception e)
                {
                    var regex = new Regex(@".*Requested dependency: ServiceType:(.*),.*");
                    var match = regex.Match(e.InnerException?.Message ?? string.Empty);
                    if (match.Success)
                    {
                        logger.Error(tag,
                            $"При создании сервиса {serviceType.FullName} произошла ошибка внедрения зависимостей",
                            e, extraData: new { UnresolvedDependency = match.Groups[1].ToString() });
                    }
                    else
                    {
                        logger.Error(tag, $"При создании сервиса {serviceType.FullName} произошла ошибка", e);
                    }

                    throw;
                }
            }
        }

        logger.Info(tag, $"Завершена проверка создаваемости сервисов ({serviceNames.Count} шт.)",
            extraData: serviceNames);
    }

    private IEnumerable<Type> GetControllers()
    {
        var controllers = statefulContainer
            .AvailableServices
            .Select(s => s.ImplementingType)
            .Where(t => t?.IsAbstract == false)
            .Where(t =>
                //чтобы лишнюю зависимость не подключать, проверим вхождение строки
                t.GetInterfaces().Any(i =>
                    i.FullName == "System.Web.Http.Controllers.IHttpController" ||
                    i.FullName == "System.Web.Mvc.IController")
            );

        return controllers;
    }
}
