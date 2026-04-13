using Moedelo.Common.Enums.Enums.ElectronicReports;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System;
using System.Text.RegularExpressions;

namespace ElectronicReportsValidator
{
    public interface IFundCodeValidator : IDI
    {
        bool Validate(OutgoingDirectionType type, string code);
    }

    [InjectAsSingleton]
    public class FundCodeValidator : IFundCodeValidator
    {
        public bool Validate(OutgoingDirectionType selectedFund, string destCode)
        {
            var pattern = GetPattern(selectedFund);

            var regex = new Regex(pattern, RegexOptions.IgnoreCase);

            var match = regex.Match(destCode);

            var success = match.Success;
            return success;
        }

        private string GetPattern(OutgoingDirectionType selectedFund)
        {
            switch (selectedFund)
            {
                case OutgoingDirectionType.PensionFund:
                    return @"^(\d\d\d-\d\d\d)$";
                case OutgoingDirectionType.StatisticalService:
                    return @"^(\d\d-\d\d-\d\d)$|^(\d\d-\d\d)$";
                case OutgoingDirectionType.SocialInsuranceFund:
                    return @"^(\d\d)$";
                case OutgoingDirectionType.TaxService:
                case OutgoingDirectionType.EdmTaxService:
                    return @"^(\d\d\d\d)$";
                default:
                    throw new ArgumentOutOfRangeException($"Argument type of OutgoingDirectionType {selectedFund} is out of range");
            }
        }
    }
}
