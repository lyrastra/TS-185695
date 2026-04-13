namespace Moedelo.Money.Enums.Extensions
{
    public static class PaymentToNaturalPersonsTypeExtensions
    {
        public static bool IsSalaryProject(this PaymentToNaturalPersonsType type)
        {
            return type == PaymentToNaturalPersonsType.SalaryProject ||
                   type == PaymentToNaturalPersonsType.GpdBySalaryProject ||
                   type == PaymentToNaturalPersonsType.DividendsBySalaryProject;
        }

        public static bool IsSalaryProject(this PaymentToNaturalPersonsType? type)
        {
            return type.HasValue && IsSalaryProject(type.Value);
        }
    }
}
