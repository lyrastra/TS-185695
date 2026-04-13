using System;
using System.Collections.Generic;
using System.Text;

namespace Moedelo.Payroll.Enums.Worker
{
    public enum WorkerNdflStatus : byte
    {
        /// <summary>
        /// Резидент.
        /// </summary>
        Resident = 0,

        /// <summary>
        /// Нерезидент.
        /// </summary>
        NonResident = 1,

        /// <summary>
        /// Высококвалифицированный специалист - нерезидент.
        /// </summary>
        HighQualifiedExpert = 2
    }
}
