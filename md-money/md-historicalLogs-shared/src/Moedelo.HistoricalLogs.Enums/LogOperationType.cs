namespace Moedelo.HistoricalLogs.Enums
{
    public enum LogOperationType
    {
        Create = 1,
        Update = 2,
        Delete = 3,
        Archive = 4,
        Duplicate = 5,
        Download = 6,
        MergedAsMaster = 7,
        MergedInto = 8,
        MergingPreview = 9,
        TryMergeTooMany = 10,
        TryMergeWithFounder = 11,
        StartProcess = 12,
        FinishProcess = 13,
        OpenFirstStep = 14,
        CompleteWizard = 15,
        Complete = 16,
        Reset = 17,
        Cancel = 18,
        SkipDuplicate = 19,
        AutoCompleteWizard = 20
    }
}
