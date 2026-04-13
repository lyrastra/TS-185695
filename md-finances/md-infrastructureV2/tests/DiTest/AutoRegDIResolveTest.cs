using System.Collections.Concurrent;
using Moedelo.DiTest;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moedelo.InfrastructureV2.Injection.Lightinject;
using Moq;
using NUnit.Framework;

namespace DiTest
{
    [TestFixture]
    public class AutoRegDIResolveTest
    {
        Mock<ILogger> loggerMock;
        Di di;

        [SetUp]
        public void SetUp()
        {
            loggerMock = new Mock<ILogger>();
            di = new Di(loggerMock.Object);
            di.Initialize();
        }

        [Test]
        public void TestResolveIDIResolver()
        {
            var obj = di.GetInstance<IDIResolver>();
            Assert.AreSame(di, obj);
        }

        [Test]
        public void TestResolveIDIInstaller()
        {
            var obj = di.GetInstance<IDIInstaller>();
            Assert.AreSame(di, obj);
        }

        [Test]
        public void TestResolveIDIChecks()
        {
            var obj = di.GetInstance<IDIChecks>();
            Assert.AreSame(di, obj);
        }
    }
}