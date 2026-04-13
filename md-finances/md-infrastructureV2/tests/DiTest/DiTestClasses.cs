using LightInject;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moedelo.InfrastructureV2.Injection.Lightinject;
using Moq;

namespace Moedelo.DiTest
{
    public class Di : DIInstaller
    {
        public Di(ILogger logger) : base(logger)
        {
        }

        protected override void RegisterBehaviour()
        {
            RegisterSingleton<ILogger>(f => new Mock<ILogger>().Object);
            RegisterPerWebRequest<TestServiceSf1, TestServiceSf1>();
            RegisterPerWebRequest<TestServiceSf2To1, TestServiceSf2To1>();
            RegisterPerWebRequest<TestServiceSf2ToSl1, TestServiceSf2ToSl1>();
            RegisterSingleton<TestServiceSl1, TestServiceSl1>();
            RegisterSingleton<TestServiceSl2To1, TestServiceSl2To1>();
            RegisterSingleton<TestServiceSl2ToSf1, TestServiceSl2ToSf1>();
            RegisterTransient<TestServiceTsl1, TestServiceTsl1>();
            RegisterTransient<TestServiceSf2ToTsl1, TestServiceSf2ToTsl1>();
            RegisterTransient<TestServiceSl2ToTsl1, TestServiceSl2ToTsl1>();
            RegisterTransient<TestServiceTsl2ToSf1, TestServiceTsl2ToSf1>();
            RegisterTransient<TestServiceTsl2ToSl1, TestServiceTsl2ToSl1>();
            RegisterSingleton<TestServiceSl3ToTsl2ToSl1, TestServiceSl3ToTsl2ToSl1>();
            RegisterSingleton<TestServiceSl3ToTsl2ToSf1, TestServiceSl3ToTsl2ToSf1>();
            RegisterPerWebRequest<TestServiceSf3ToTsl2ToSl1, TestServiceSf3ToTsl2ToSl1>();
            RegisterPerWebRequest<TestServiceSf3ToTsl2ToSf1, TestServiceSf3ToTsl2ToSf1>();
        }

        public Scope BeginScope()
        {
            return StateFullContainer.BeginScope();
        }
    }

    public class TestServiceSf1
    {
        public string Test()
        {
            return GetType().Name;
        }
    }
    public class TestServiceSl1
    {
        public string Test()
        {
            return GetType().Name;
        }
    }
    public class TestServiceTsl1
    {
        public string Test()
        {
            return GetType().Name;
        }
    }
    public class TestServiceSf2To1
    {
        private readonly TestServiceSf1 service;

        public TestServiceSf2To1(TestServiceSf1 service)
        {
            this.service = service;
        }

        public string Test()
        {
            return GetType().Name + " " + service.Test();
        }
    }

    public class TestServiceSl2To1
    {
        private readonly TestServiceSl1 service;

        public TestServiceSl2To1(TestServiceSl1 service)
        {
            this.service = service;
        }

        public string Test()
        {
            return GetType().Name + " " + service.Test();
        }
    }

    public class TestServiceSl2ToSf1
    {
        private readonly TestServiceSf1 service;

        public TestServiceSl2ToSf1(TestServiceSf1 service)
        {
            this.service = service;
        }

        public string Test()
        {
            return GetType().Name + " " + service.Test();
        }
    }
    public class TestServiceSf2ToSl1
    {
        private readonly TestServiceSl1 service;

        public TestServiceSf2ToSl1(TestServiceSl1 service)
        {
            this.service = service;
        }

        public string Test()
        {
            return GetType().Name + " " + service.Test();
        }
    }
    public class TestServiceSf2ToTsl1
    {
        private readonly TestServiceTsl1 service;

        public TestServiceSf2ToTsl1(TestServiceTsl1 service)
        {
            this.service = service;
        }

        public string Test()
        {
            return GetType().Name + " " + service.Test();
        }
    }
    public class TestServiceSl2ToTsl1
    {
        private readonly TestServiceTsl1 service;

        public TestServiceSl2ToTsl1(TestServiceTsl1 service)
        {
            this.service = service;
        }

        public string Test()
        {
            return GetType().Name + " " + service.Test();
        }
    }

    public class TestServiceTsl2ToSl1
    {
        private readonly TestServiceSl1 service;

        public TestServiceTsl2ToSl1(TestServiceSl1 service)
        {
            this.service = service;
        }

        public string Test()
        {
            return GetType().Name + " " + service.Test();
        }
    }
    public class TestServiceTsl2ToSf1
    {
        private readonly TestServiceSf1 service;

        public TestServiceTsl2ToSf1(TestServiceSf1 service)
        {
            this.service = service;
        }

        public string Test()
        {
            return GetType().Name + " " + service.Test();
        }
    }

    public class TestServiceSl3ToTsl2ToSl1
    {
        private readonly TestServiceTsl2ToSl1 service;

        public TestServiceSl3ToTsl2ToSl1(TestServiceTsl2ToSl1 service)
        {
            this.service = service;
        }

        public string Test()
        {
            return GetType().Name + " " + service.Test();
        }
    }
    public class TestServiceSf3ToTsl2ToSl1
    {
        private readonly TestServiceTsl2ToSl1 service;

        public TestServiceSf3ToTsl2ToSl1(TestServiceTsl2ToSl1 service)
        {
            this.service = service;
        }

        public string Test()
        {
            return GetType().Name + " " + service.Test();
        }
    }
    public class TestServiceSl3ToTsl2ToSf1
    {
        private readonly TestServiceTsl2ToSf1 service;

        public TestServiceSl3ToTsl2ToSf1(TestServiceTsl2ToSf1 service)
        {
            this.service = service;
        }

        public string Test()
        {
            return GetType().Name + " " + service.Test();
        }
    }
    public class TestServiceSf3ToTsl2ToSf1
    {
        private readonly TestServiceTsl2ToSf1 service;

        public TestServiceSf3ToTsl2ToSf1(TestServiceTsl2ToSf1 service)
        {
            this.service = service;
        }

        public string Test()
        {
            return GetType().Name + " " + service.Test();
        }
    }
}
