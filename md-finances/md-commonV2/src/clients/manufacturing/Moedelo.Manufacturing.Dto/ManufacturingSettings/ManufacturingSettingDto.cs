namespace Moedelo.Manufacturing.Dto
{
    public class ManufacturingSettingDto
    {
        /// <summary>
        /// Включено ли производство
        /// </summary>
        public bool Enabled { get; set; }
        
        /// <summary>
        /// Можно ли включить или выключить поизводство
        /// </summary>
        public bool CanChange { get; set; }
        
        
        /// <summary>
        /// Можно ли менять колличество основного сырья в отчетах о производстве
        /// </summary>
        public bool CanChangeProductCount { get; set; }
    }
}