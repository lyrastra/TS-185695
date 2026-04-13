using System;
using Moedelo.Common.Enums.Enums.Consultations;

namespace Moedelo.Consultations.Dto.Message
{
    public class ConsultationMessageViewModelDto
    {
        public int Id { get; set; }

        /// <summary> ID консультации, к которой относится сообщение </summary>
        public int ThreadId { get; set; }

        /// <summary> Дата сообщения </summary>
        public DateTime Date { get; set; }

        /// <summary> Текст сообщения </summary>
        public string Text { get; set; }

        /// <summary> Рейтинг (оценка ответа пользователем) </summary>
        public ConsultationAnswerRating Rating { get; set; }

        /// <summary> Статус ответа (создан/подтвержден проверяющим консультантом) </summary>
        public ConsultationAnswerStatus Status { get; set; }

        /// <summary> Тип сообщения, вопрос или ответ </summary>
        public ConsultationMessageType Type { get; set; }

        // /// <summary> ID консультанта в таблице Users, ответившего на вопрос </summary>
        // public int? ConsultantUserId { get; set; }
        //
        // /// <summary> ID консультанта в таблице Users, подтвердившего ответ </summary>
        // public int? ApproverUserId { get; set; }
        //
        // /// <summary> Дата подтверждения ответа </summary>
        // public DateTime? DateApproved { get; set; }
        //
        // /// <summary> Дата ответа консультанта </summary>
        // public DateTime? AnswerDate { get; set; }
        //
        // /// <summary> Дата подготовки ответа </summary>
        // public DateTime? DateToAnswerConsultant { get; set; }

        /// <summary> Количество прикрепленных файлов </summary>
        public virtual int AttachmentsCount { get; set; }
    }
}
