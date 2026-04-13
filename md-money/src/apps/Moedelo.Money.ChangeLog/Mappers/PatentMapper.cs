using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Money.ChangeLog.Mappers
{
    static class PatentMapper
    {
        public static string MapToName(this PatentWithoutAdditionalDataDto patent, long? patentId)
        {
            if (patentId == null)
            {
                return null;
            }
            return patent?.ShortName ?? MapToTechnicalName(patentId.Value);
        }

        private static string MapToTechnicalName(long patentId)
        {
            return $"Неизвестный патент #{patentId} (тех.идентификатор)";
        }
    }
}
