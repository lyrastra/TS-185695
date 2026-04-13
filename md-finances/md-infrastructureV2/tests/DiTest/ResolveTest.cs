using System;
using Moedelo.DiTest;
using Moedelo.InfrastructureV2.Domain.Interfaces.Context;
using Moedelo.InfrastructureV2.Domain.Interfaces.Http;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moq;
using NUnit.Framework;

namespace DiTest
{
    [TestFixture]
    public class ResolveTest
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
        public void TestServiceSf2To1()
        {
            using (var scope = di.BeginScope())
            {
                var obj = di.GetInstance<TestServiceSf2To1>();
                Assert.NotNull(obj);
            }
        }
        [Test]
        public void TestServiceSf2ToSl1()
        {
            using (var scope = di.BeginScope())
            {
                var obj = di.GetInstance<TestServiceSf2ToSl1>();
                Assert.NotNull(obj);
            }
        }
        [Test]
        public void TestServiceSl2To1()
        {
            using (var scope = di.BeginScope())
            {
                var obj = di.GetInstance<TestServiceSl2To1>();
                Assert.NotNull(obj);
            }
        }
        [Test]
        public void TestServiceSl2ToSf1()
        {
            using (var scope = di.BeginScope())
            {
                Assert.Throws(Is.TypeOf<InvalidOperationException>(), () =>
                {
                    var obj = di.GetInstance<TestServiceSl2ToSf1>();
                });
            }
        }

        [Test]
        public void TestServiceTsl1()
        {
            using (var scope = di.BeginScope())
            {
                var obj = di.GetInstance<TestServiceTsl1>();
                Assert.NotNull(obj);
            }
        }
        [Test]
        public void TestServiceSf2ToTsl1()
        {
            using (var scope = di.BeginScope())
            {
                var obj = di.GetInstance<TestServiceSf2ToTsl1>();
                Assert.NotNull(obj);
            }
        }
        [Test]
        public void TestServiceSl2ToTsl1()
        {
            using (var scope = di.BeginScope())
            {
                var obj = di.GetInstance<TestServiceSl2ToTsl1>();
                Assert.NotNull(obj);
            }
        }
        [Test]
        public void TestServiceSl3ToTsl2ToSl1()
        {
            using (var scope = di.BeginScope())
            {
                var obj = di.GetInstance<TestServiceSl3ToTsl2ToSl1>();
                Assert.NotNull(obj);
            }
        }
        [Test]
        public void TestServiceSl3ToTsl2ToSf1()
        {
            using (var scope = di.BeginScope())
            {
                Assert.Throws(Is.TypeOf<InvalidOperationException>(), () =>
                {
                    var obj = di.GetInstance<TestServiceSl3ToTsl2ToSf1>();
                });
            }
        }
        [Test]
        public void TestServiceSf3ToTsl2ToSl1()
        {
            using (var scope = di.BeginScope())
            {
                var obj = di.GetInstance<TestServiceSf3ToTsl2ToSl1>();
                Assert.NotNull(obj);
            }
        }
        [Test]
        public void TestServiceSf3ToTsl2ToSf1()
        {
            using (var scope = di.BeginScope())
            {
                var obj = di.GetInstance<TestServiceSf3ToTsl2ToSf1>();
                Assert.NotNull(obj);
            }
        }
    }
}