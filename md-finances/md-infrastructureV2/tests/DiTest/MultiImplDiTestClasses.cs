using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using LightInject;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moedelo.InfrastructureV2.Injection.Lightinject;
using Moedelo.InfrastructureV2.Logging;

namespace Moedelo.DiTest
{
    public class MultiImplDi : DIInstaller
    {
        public MultiImplDi(ILogger logger) : base(logger)
        {
        }

        protected override void RegisterBehaviour()
        {
            StateFullContainer.ScopeManagerProvider = new PerLogicalCallContextScopeManagerProvider();
            StateLessContainer.ScopeManagerProvider = new PerLogicalCallContextScopeManagerProvider();

            RegisterSingleton<ILogger, Logger>();
            RegisterSingleton<IDIResolver>(a => this);

            RegisterPerWebRequest<ITestLogger, TestLoggerStateFull>();
            RegisterSingleton<ITestLogger, TestLoggerStateLess>();

            RegisterPerWebRequest<TestStateFullService, TestStateFullService>();
            RegisterSingleton<TestStateLessService, TestStateLessService>();
        }

        public Scope BeginScope()
        {
            return StateFullContainer.BeginScope();
        }
    }

    public class InternalLogger
    {
        public static ConcurrentBag<string> logs = new ConcurrentBag<string>();
    } 
    
    public interface ITestLogger
    {
        void Log(string message);
    }

    public class TestLoggerStateFull : ITestLogger
    {
        public void Log(string message)
        {
            var str = GetHashCode() + ":" + GetType().Name + ":" + message;
            InternalLogger.logs.Add(str);
            Console.WriteLine(str);
        }
    }

    public class TestLoggerStateLess : ITestLogger
    {
        private readonly IDIResolver di;
        private readonly AsyncLocal<ITestLogger> local = new AsyncLocal<ITestLogger>();

        public TestLoggerStateLess(IDIResolver di)
        {
            this.di = di;
        }

        private ITestLogger innerLogger => local.Value ?? GetLocal();

        private ITestLogger GetLocal()
        {
            var logger = di.GetInstance<ITestLogger>();
            local.Value = logger;
            return logger;
        }

        public void Log(string message)
        {
            var str = GetHashCode() + ":" + GetType().Name + ":" + message;
            InternalLogger.logs.Add(str);
            Console.WriteLine(str);

            innerLogger.Log(message);
        }
    }

    public class TestStateFullService
    {
        private readonly ITestLogger logger;

        public TestStateFullService(ITestLogger logger)
        {
            this.logger = logger;
        }

        public void Test()
        {
            logger.Log(GetType().Name);
        }
    }

    public class TestStateLessService
    {
        private readonly ITestLogger logger;

        public TestStateLessService(ITestLogger logger)
        {
            this.logger = logger;
        }

        public void Test(int? id = null)
        {
            var prefix = id.HasValue ? (id + ":") : string.Empty; 
            logger.Log(prefix + GetType().Name);
        }
    }
}