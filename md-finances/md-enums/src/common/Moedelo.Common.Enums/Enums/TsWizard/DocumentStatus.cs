using System;

namespace Moedelo.Edm.Backend.Domain.Enums
{
    public enum DocumentStatus : byte
    {
        Default = 0,

        /// <summary>
        /// Непрочитанный
        /// </summary>
        New = 1,

        /// <summary>
        /// На подписании
        /// </summary>
        ProcessingSigning = 2,

        /// <summary>
        /// Подписанный
        /// </summary>
        Signed = 3,

        /// <summary>
        /// Отклоненный
        /// </summary>
        Rejected = 4,

        /// <summary>
        /// На аннулировании
        /// </summary>
        ProcessingCancellation = 5,

        /// <summary>
        /// Аннулированный
        /// </summary>
        Cancelled = 6,

        /// <summary>
        /// На подготовке (только не формализованные). Только для исходящих
        /// </summary>
        [Obsolete("Ранее не использовался. И начинать нигде использовать не будем")]
        Preparation = 7,

        /// <summary>
        /// Выставлен (для документов, не требующих ответной подписи)
        /// </summary>
        Completed = 8, // хрен знает какое слово правильно отражает суть этого явления
        
        /// <summary>
        /// Документ отправлен в стек и ожидает подписания. После подтверждения в myDSS документу выставляется статус
        /// Completed и он переходит на основную вкладку
        /// </summary>
        StekProcessing = 11,
        
        /// <summary>
        /// Документ отправлен в стек и ожидает подписания. После подтверждения в myDSS документу выставляется статус
        /// ProcessingSigning он переходит на основную вкладку
        /// </summary>
        StekProcessingSignable = 12,
        /// <summary>
        /// Документ отклонено пользователем, возможен перезапуск отправки
        /// </summary>
        StekDssSigningRefused = 13,
        
        /// <summary>
        /// Ошибка во время подписания (ошибка может быить как нашего УЦ так и СТЭКа) 
        /// </summary>
        StekDssSigningError = 14,
        
          
        AcceptanceWaitOnMyDssSigning = 15,
        RejectionWaitOnMyDssSigning = 16
    }
}
