using System;

namespace Moedelo.Common.Logging.Options
{
    public class FileLoggerOptions
    {
        private string fileName = "UndefinedAppName";

        public string FileName
        {
            get => fileName;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(nameof(value));
                }

                fileName = value;
            }
        }
    }
}