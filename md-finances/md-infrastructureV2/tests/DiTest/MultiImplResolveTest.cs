using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.DiTest;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moq;
using NUnit.Framework;

namespace DiTest
{
    [TestFixture]
    public class MultiImplResolveTest
    {
        Mock<ILogger> loggerMock;
        MultiImplDi di;

        [SetUp]
        public void SetUp()
        {
            InternalLogger.logs = new ConcurrentBag<string>();
            loggerMock = new Mock<ILogger>();
            di = new MultiImplDi(loggerMock.Object);
            di.Initialize();
        }

        [Test]
        public void Test0ServiceStateFull()
        {
            using (var scope = di.BeginScope())
            {
                var obj = di.GetInstance<TestStateFullService>();
                Assert.NotNull(obj);
                obj.Test();

                Assert.IsFalse(InternalLogger.logs.IsEmpty);
            }
        }

        [Test]
        public void Test1ServiceStateLess()
        {
            using (var scope = di.BeginScope())
            {
                var obj = di.GetInstance<TestStateLessService>();
                Assert.NotNull(obj);
                obj.Test();
            }

            Assert.IsFalse(InternalLogger.logs.IsEmpty);
            var countDifferentHashcodes = InternalLogger.logs
                .Select(s => s.Split(':')[0]) //get hashcodes
                .Distinct()
                .Count();
            //check different hashcodes
            Assert.AreEqual(InternalLogger.logs.Count, countDifferentHashcodes);
        }

        [Test]
        public void Test2ServiceStateLess()
        {
            Task.Run(() =>
            {
                using (var scope = di.BeginScope())
                {
                    var obj = di.GetInstance<TestStateLessService>();
                    Assert.NotNull(obj);
                    obj.Test();
                }
            }).Wait();

            Assert.IsFalse(InternalLogger.logs.IsEmpty);
            var countDifferentHashcodes = InternalLogger.logs
                .Select(s => s.Split(':')[0]) //get hashcodes
                .Distinct()
                .Count();
            //check different hashcodes
            Assert.AreEqual(InternalLogger.logs.Count, countDifferentHashcodes);
        }

        [Test]
        public void Test3ServiceStateLess()
        {
            var ids = new[] {1, 2, 3, 4, 5, 6, 7, 8};

            var tasks = ids.Select(id =>
                Task.Run(() =>
                {
                    using (var scope = di.BeginScope())
                    {
                        var obj = di.GetInstance<TestStateLessService>();
                        Assert.NotNull(obj);
                        obj.Test(id);
                    }
                }));
            Task.WaitAll(tasks.ToArray());

            //check repeate {hashcode}:TestLoggerStateLess:
            Assert.IsFalse(InternalLogger.logs.IsEmpty);
            var countDifferentHashcodes = InternalLogger.logs
                .Select(s =>
                {
                    var arr = s.Split(':');
                    if (arr[1] != "TestLoggerStateLess")
                    {
                        return string.Empty;
                    }
                    return $"{arr[0]}:{arr[1]}";
                }) //get hashcodes
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Distinct()
                .Count();
            //check different hashcodes
            Assert.AreEqual(1, countDifferentHashcodes);
        }

        [Test]
        public void Test4ServiceStateLess()
        {
            var ids = new[] {1, 2, 3, 4};

            var tasks = ids.Select(async id =>
            {
                using (var scope = di.BeginScope())
                {
                    var obj = di.GetInstance<TestStateLessService>();
                    Assert.NotNull(obj);
                    obj.Test(id);
                    await Task.Delay(200).ConfigureAwait(false);
                    await Task.Run(() => obj.Test(id)).ConfigureAwait(false);
                }
            });
            Task.WaitAll(tasks.ToArray());

            //check repeate {hashcode}:TestLoggerStateFull:{ID}:TestStateLessService
            Assert.IsFalse(InternalLogger.logs.IsEmpty);
            var countDifferentHashcodes = InternalLogger.logs
                .Select(s =>
                {
                    var arr = s.Split(':');
                    if (arr[1] != "TestLoggerStateFull" || arr[3] != "TestStateLessService")
                    {
                        return string.Empty;
                    }
                    return $"{arr[0]}:{arr[1]}:{arr[2]}:{arr[3]}";
                }) //get hashcodes
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Distinct()
                .Count();
            //check different hashcodes
            Assert.AreEqual(4, countDifferentHashcodes);
        }

        [Test]
        public void Test5ServiceStateLess()
        {
            var ids = new[] {1, 2, 3, 4};

            var tasks = ids.Select(id =>
                Task.Run(async () =>
                {
                    using (var scope = di.BeginScope())
                    {
                        var obj = di.GetInstance<TestStateLessService>();
                        Assert.NotNull(obj);
                        obj.Test(id);
                        await Task.Delay(200).ConfigureAwait(false);
                        await Task.Run(() => obj.Test(id)).ConfigureAwait(false);
                    }
                }));
            Task.WaitAll(tasks.ToArray());

            //check repeate {hashcode}:TestLoggerStateFull:{ID}:TestStateLessService
            Assert.IsFalse(InternalLogger.logs.IsEmpty);
            var countDifferentHashcodes = InternalLogger.logs
                .Select(s =>
                {
                    var arr = s.Split(':');
                    if (arr[1] != "TestLoggerStateFull" || arr[3] != "TestStateLessService")
                    {
                        return string.Empty;
                    }
                    return $"{arr[0]}:{arr[1]}:{arr[2]}:{arr[3]}";
                }) //get hashcodes
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .GroupBy(s => s)
                .Select(g => g.Count())
                .ToList();
            //check different hashcodes
            foreach (var count in countDifferentHashcodes)
            {
                Assert.AreEqual(2, count);
            }
        }
    }
}