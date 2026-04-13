namespace Moedelo.Common.Enums.Enums.EntityTypes
{
    public enum TransferType
    {
        /// <summary> Перевод фирмы с БИЗ на УС </summary>
        TransferFirm = 0,
        /// <summary> Копирование документов у ранее переведенной фирмы с БИЗ на УС </summary>
        CopyDocs = 1,
        /// <summary> Возврат, ранее переведенной фирмы с УС на БИЗ </summary>
        RollbackFirmToBiz = 2,
        /// <summary> Копирование наличных денежных операций (касса) у ранее переведенной фирмы с БИЗ на УС </summary>
        CopyCashOperations = 3,
        /// <summary> Заполнение остатков </summary>
        FillRemains = 4,
        /// <summary> Заполнение остатков по зарплате </summary>
        FillPayrollRemains = 5,


        /// <summary> Фикс возврата, ранее переведенной фирмы с УС на БИЗ </summary>
        FixRollbackFirmToBiz = 99,
    }
}