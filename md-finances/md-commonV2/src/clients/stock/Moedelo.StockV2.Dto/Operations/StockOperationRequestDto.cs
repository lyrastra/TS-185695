using System;

namespace Moedelo.StockV2.Dto.Operations
{
    public class StockOperationRequestDto
    {
        /// <summary>
        /// Начало периода
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Конец периода
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Проверять себестоимость на отрицательные значения
        /// </summary>
        public bool CheckNegativeOperations { get; set; }

        /// <summary>
        /// С учётом забытых документов
        /// </summary>
        public bool WithForgotten { get; set; }

        /// <summary>
        /// Себестоимость по документу-источкнику для УКД из прошлых лет
        /// </summary>
        public bool UkdSelfCostBySourceForDifferentYears { get; set; }

        /// <summary>
        /// Если между годами была смена типа УСН с 6% на 15%, то не учитываем себестоимость остатков в прошлом году
        /// </summary>
        public int? SuppressYearForSelfCost { get; set; }
    }
}