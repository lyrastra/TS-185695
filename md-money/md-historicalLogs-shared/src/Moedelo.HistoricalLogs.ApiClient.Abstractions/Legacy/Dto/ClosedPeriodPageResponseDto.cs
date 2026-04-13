﻿using System.Collections.Generic;

 namespace Moedelo.HistoricalLogs.ApiClient.Abstractions.Legacy.Dto
{
    public class ClosedPeriodPageResponseDto
    {
        public ClosedPeriodPageResponseDto(IReadOnlyList<ClosedPeriodLogResponseDto> data, int totalCount)
        {
            this.Data = data;
            TotalCount = totalCount;
        }

        public IReadOnlyList<ClosedPeriodLogResponseDto> Data { get; set; }

        public int TotalCount { get; set; }
    }
}