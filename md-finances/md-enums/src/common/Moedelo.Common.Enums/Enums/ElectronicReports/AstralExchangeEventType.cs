namespace Moedelo.Common.Enums.Enums.ElectronicReports
{
    public enum AstralExchangeEventType
    {
        GlobalException = 0,
        AvailableInList = 1,

        CheckingOutgoingUid = 2,
        CheckingIncommingLetter = 3,
        CheckingUnknownUid = 4,

        ReceivedFiles = 5,
        ErrorGettingFiles = 6,
        ErrorProcessingFiles = 7,
        ConfirmedFiles = 8,
        ErrorConfirmingFiles = 9,
        SavedToLostDirectory = 10,
        EndGettingSession = 11,

        GlobalQueueCount = 12
    }
}
