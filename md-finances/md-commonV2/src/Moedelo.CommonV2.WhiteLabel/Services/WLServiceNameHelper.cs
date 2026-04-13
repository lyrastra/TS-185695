using Moedelo.Common.Enums.Enums.Products;
using System;

namespace Moedelo.CommonV2.WhiteLabel.Services
{
    public static class WLServiceNameHelper
    {
        public static string GetServiceName(WLProductPartition productPartition)
        {
            switch (productPartition)
            {
                case WLProductPartition.SkbBank:
                    {
                        return "Облачная бухгалтерия ДелоБанк";
                    }
                case WLProductPartition.Buro:
                    {
                        return "Бюро бухгалтера";
                    }
                case WLProductPartition.Sberbank:
                    {
                        return "Моя Бухгалтерия Онлайн";
                    }
                case WLProductPartition.DefaultBiz:
                    {
                        return "Моё дело";
                    }
                default:
                    throw new ArgumentOutOfRangeException(nameof(productPartition), productPartition, null);
            }
        }
    }
}
