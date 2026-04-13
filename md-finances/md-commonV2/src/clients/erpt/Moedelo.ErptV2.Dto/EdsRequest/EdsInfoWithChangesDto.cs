using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.ElectronicReports;

namespace Moedelo.ErptV2.Dto.EdsRequest
{
    public sealed class EdsInfoWithChangesDto
    {
        public EdsRequestType EdsRequestType { get; }
        public DateTime EdsRequestDate { get; }
        public EdsProvider EdsProvider { get; }
        public string AbnGuid { get; }
        public string StekUserId { get; }
        public string TariffName { get; }
        public IReadOnlyList<EdsRequisiteWithChangeDto> Requisites { get; }
        public IReadOnlyList<AdditionalFnsChangeDto> AdditionalFns { get; }
        public string AstralPfrRoutingDirection { get; }


        public EdsInfoWithChangesDto(EdsRequestType edsRequestType, DateTime edsRequestDate, EdsProvider edsProvider,
            string abnGuid, string stekUserId, string tariffName,
            IReadOnlyList<EdsRequisiteWithChangeDto> requisites, IReadOnlyList<AdditionalFnsChangeDto> additionalFns, string astralPfrRoutingDirection = null)
        {
            EdsRequestType = edsRequestType;
            EdsRequestDate = edsRequestDate;
            EdsProvider = edsProvider;
            AbnGuid = abnGuid;
            StekUserId = stekUserId;
            TariffName = tariffName;
            Requisites = requisites;
            AdditionalFns = additionalFns;
            AstralPfrRoutingDirection = astralPfrRoutingDirection;
        }
    }
}