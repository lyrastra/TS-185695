namespace Moedelo.Common.Logging.Logger
{
    internal sealed class CallerInfo
    {
        public CallerInfo(string callerMemberName, string callerFilePath, int callerLineNumber)
        {
            CallerMemberName = callerMemberName;
            CallerFilePath = callerFilePath;
            CallerLineNumber = callerLineNumber;
        }

        public string CallerMemberName { get; }

        public string CallerFilePath { get; }

        public int CallerLineNumber { get; }
    }
}