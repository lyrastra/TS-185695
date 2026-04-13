namespace Moedelo.Consultations.Dto.ConsultationUsage
{
    /// <summary> Информация об использовании консультаций </summary>
    public class ConsultationUsageResponseDto
    {
        /// <summary> Имеет ли возможность задавать неограниченное количество вопросов в консультацию </summary>
        public bool HasNoLimitedCount { get; set; }

        /// <summary> Имеет возможность задавать неограниченное количество вопросов по юридическим консультациям /// </summary>
        public bool HasNoLimitedJuridicalCount { get; set; }

        /// <summary> Количество использованных бухгалтерских консультаций </summary>
        public int UsedCount { get; set; }

        /// <summary> Максимальное количество бухгалтерских консультаций на текущем тарифе </summary>
        public int? MaxCount { get; set; }

        /// <summary> Количество использованных юридических консультаций </summary>
        public int UsedJuridicalCount { get; set; }

        /// <summary> Максимальное количество юридических консультаций на текущем тарифе </summary>
        public int? MaxJuridicalCount { get; set; }
    }
}