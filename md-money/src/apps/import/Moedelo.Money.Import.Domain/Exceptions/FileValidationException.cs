using System;

namespace Moedelo.Money.Import.Domain.Exceptions
{
    public class FileValidationException : Exception
    {
        public FileValidationException(params string[] errors)
            : base("В загружаемом файле обнаружены ошибки")
        {
            Errors = errors;
        }

        public string[] Errors { get; }
    }
}
