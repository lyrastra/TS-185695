using System;

namespace Moedelo.CommonV2.Cells.Models.Settings
{
    public sealed class ReportAutoHeightSettings
    {
        public int StartRowIndex { get; private set; }

        public int EndRowIndex { get; private set; }

        public ReportAutoHeightSettings(int startRowIndex, int endRowIndex)
        {
            if (startRowIndex > endRowIndex)
                throw new InvalidOperationException($"Parameter {nameof(startRowIndex)} value cannot be more than {nameof(endRowIndex)} value");

            StartRowIndex = startRowIndex;
            EndRowIndex = endRowIndex;
        }
    }
}