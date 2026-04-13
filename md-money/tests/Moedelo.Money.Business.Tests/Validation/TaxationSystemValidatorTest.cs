using FluentAssertions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.TaxationSystems;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Enums;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Moedelo.Money.Business.Patent;
using Moedelo.Money.Business.FirmRequisites;

namespace Moedelo.Money.Business.Tests.Validation
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    public class TaxationSystemValidatorTest
    {
        private Mock<ITaxationSystemTypeReader> taxSystemReader;
        private Mock<IFirmRequisitesReader> requisitesReader;
        private Mock<IPatentReader> patentReader;

        private TaxationSystemValidator validator;

        [SetUp]
        public void Setup()
        {
            taxSystemReader = new Mock<ITaxationSystemTypeReader>();
            requisitesReader = new Mock<IFirmRequisitesReader>();
            patentReader = new Mock<IPatentReader>();

            validator = new TaxationSystemValidator(taxSystemReader.Object, requisitesReader.Object, patentReader.Object);
        }

        [Test]
        [TestCase(TaxationSystemType.Usn, TaxationSystemType.UsnAndEnvd)]
        [TestCase(TaxationSystemType.Usn, TaxationSystemType.Envd)]
        [TestCase(TaxationSystemType.Usn, TaxationSystemType.Osno)]
        [TestCase(TaxationSystemType.Usn, TaxationSystemType.OsnoAndEnvd)]
        [TestCase(TaxationSystemType.UsnAndEnvd, TaxationSystemType.Osno)]
        [TestCase(TaxationSystemType.UsnAndEnvd, TaxationSystemType.OsnoAndEnvd)]
        [TestCase(TaxationSystemType.Envd, TaxationSystemType.UsnAndEnvd)]
        [TestCase(TaxationSystemType.Envd, TaxationSystemType.Osno)]
        [TestCase(TaxationSystemType.Envd, TaxationSystemType.OsnoAndEnvd)]
        [TestCase(TaxationSystemType.Osno, TaxationSystemType.Usn)]
        [TestCase(TaxationSystemType.Osno, TaxationSystemType.UsnAndEnvd)]
        [TestCase(TaxationSystemType.Osno, TaxationSystemType.Envd)]
        [TestCase(TaxationSystemType.Osno, TaxationSystemType.OsnoAndEnvd)]
        [TestCase(TaxationSystemType.OsnoAndEnvd, TaxationSystemType.Usn)]
        [TestCase(TaxationSystemType.OsnoAndEnvd, TaxationSystemType.UsnAndEnvd)]
        [TestCase(TaxationSystemType.OsnoAndEnvd, TaxationSystemType.Patent)]
        public void ValidateAsync_ShouldThrowException_IfTaxationSystemIsNotAvailable(TaxationSystemType currentTaxSystem, TaxationSystemType taxSystem)
        {
            //arrange
            taxSystemReader.Setup(x => x.GetByYearAsync(It.IsAny<int>()))
                .ReturnsAsync(currentTaxSystem);

            //act
            Func<Task> validateTask = () => validator.ValidateAsync(DateTime.Today, taxSystem);

            //assert
            validateTask.Should().ThrowExactlyAsync<BusinessValidationException>()
                .WithMessage("Нельзя использовать указанную СНО")
                .Result.And.Name.Should().Be("TaxationSystemType");
        }

        [Test]
        [TestCase(TaxationSystemType.Usn, TaxationSystemType.Usn)]
        [TestCase(TaxationSystemType.UsnAndEnvd, TaxationSystemType.Usn)]
        [TestCase(TaxationSystemType.UsnAndEnvd, TaxationSystemType.Envd)]
        [TestCase(TaxationSystemType.Envd, TaxationSystemType.Envd)]
        [TestCase(TaxationSystemType.Osno, TaxationSystemType.Osno)]
        [TestCase(TaxationSystemType.OsnoAndEnvd, TaxationSystemType.Osno)]
        [TestCase(TaxationSystemType.OsnoAndEnvd, TaxationSystemType.Envd)]
        public async Task ValidateAsync_ShouldPass_IfTaxationSystemIsAvailable(TaxationSystemType currentTaxSystem, TaxationSystemType taxSystem)
        {
            //arrange
            taxSystemReader.Setup(x => x.GetByYearAsync(It.IsAny<int>()))
                .ReturnsAsync(currentTaxSystem);

            //act
            await validator.ValidateAsync(DateTime.Today, taxSystem);
        }

        [Test]
        [TestCase(TaxationSystemType.Usn)]
        [TestCase(TaxationSystemType.Envd)]
        [TestCase(TaxationSystemType.Osno)]
        [TestCase(TaxationSystemType.UsnAndEnvd)]
        public void ValidateAsync_ShouldThrowException_IfOooWithPatent(TaxationSystemType currentTaxSystem)
        {
            //arrange
            taxSystemReader.Setup(x => x.GetByYearAsync(It.IsAny<int>()))
                .ReturnsAsync(currentTaxSystem);

            requisitesReader.Setup(x => x.IsOooAsync())
                .ReturnsAsync(true);

            patentReader.Setup(x => x.IsAnyExists(It.IsAny<DateTime>()))
                .ReturnsAsync(true);

            //act
            Func<Task> validateTask = () => validator.ValidateAsync(DateTime.Today, TaxationSystemType.Patent);

            //assert
            validateTask.Should().ThrowExactlyAsync<BusinessValidationException>()
                .WithMessage("Нельзя использовать патент для ООО")
                .Result.And.Name.Should().Be("TaxationSystemType");
        }

        [Test]
        [TestCase(TaxationSystemType.Usn)]
        [TestCase(TaxationSystemType.Envd)]
        [TestCase(TaxationSystemType.UsnAndEnvd)]
        public void ValidateAsync_ShouldThrowException_IfPatentDosnNotExistsOnDate(TaxationSystemType currentTaxSystem)
        {
            //arrange
            taxSystemReader.Setup(x => x.GetByYearAsync(It.IsAny<int>()))
                .ReturnsAsync(currentTaxSystem);

            patentReader.Setup(x => x.IsAnyExists(It.IsAny<DateTime>()))
                .ReturnsAsync(false);

            //act
            Func<Task> validateTask = () => validator.ValidateAsync(DateTime.Today, TaxationSystemType.Patent);

            //assert
            validateTask.Should().ThrowExactlyAsync<BusinessValidationException>()
                .WithMessage("Нет патента на указанную дату")
                .Result.And.Name.Should().Be("TaxationSystemType");
        }

        [Test]
        [TestCase(TaxationSystemType.Usn)]
        [TestCase(TaxationSystemType.Envd)]
        [TestCase(TaxationSystemType.UsnAndEnvd)]
        public async Task ValidateAsync_ShouldPass_IfPatentIsExistsOnDate(TaxationSystemType currentTaxSystem)
        {
            //arrange
            taxSystemReader.Setup(x => x.GetByYearAsync(It.IsAny<int>()))
                .ReturnsAsync(currentTaxSystem);

            patentReader.Setup(x => x.IsAnyExists(It.IsAny<DateTime>()))
                .ReturnsAsync(true);

            //act
            await validator.ValidateAsync(DateTime.Today, TaxationSystemType.Patent);
        }
    }
}
