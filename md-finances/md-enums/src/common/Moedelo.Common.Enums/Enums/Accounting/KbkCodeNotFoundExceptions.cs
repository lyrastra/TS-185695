using System;

namespace Moedelo.Common.Enums.Enums.Accounting
{
    /// <summary>
    /// Исключение, если не генерируется код сохранения для конкретного типа КБК
    /// </summary>
    public class KbkCodeNotFoundExceptions:Exception
    {
        public KbkCodeNotFoundExceptions(KbkType kbkType):base(string.Format("Не найден код КБК для типа {0}", kbkType))
        {
        }
    }
}
