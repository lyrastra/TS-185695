using System;
using System.Collections.Generic;
using Moedelo.Money.Import.Domain.Models.PurseOperation;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Money.Import.Domain.Interfaces;

public interface IPurseOperationsParserFromExcel
{
    public (IList<PurseOperationFromExcel>, IList<string>) GetOperations(PurseOperationImportRequest request,
        IList<TaxationSystemDto> taxationSystems, IList<PatentWithoutAdditionalDataDto> patents, DateTime? registrationDate);
}