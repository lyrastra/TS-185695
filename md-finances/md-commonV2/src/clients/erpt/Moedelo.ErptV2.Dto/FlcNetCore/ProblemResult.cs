namespace Moedelo.ErptV2.Dto.FlcNetCore
{
    public class ProblemResult
    {
        /// <summary>Важность найденной проблемы.</summary>
        public ProblemSeverity Severity { get; }

        /// <summary>Категория найденной проблемы.</summary>
        public ProblemCategory Category { get; }

        /// <summary>Описание найденной проблемы.</summary>
        public string Message { get; }

        public ProblemResult(ProblemSeverity severity, ProblemCategory category, string message)
        {
            Severity = severity;
            Category = category;
            Message = message;
        }
    }
}