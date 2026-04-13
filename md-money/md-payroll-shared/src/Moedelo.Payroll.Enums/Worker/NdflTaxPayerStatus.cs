namespace Moedelo.Payroll.Enums.Worker
{
    public enum NdflTaxPayerStatus
    {
        /// <summary>
        /// Налоговый резидент России
        /// </summary>
        Russian = 1,

        /// <summary>
        /// Иностранец, не являющийся налоговым резидентом России
        /// </summary>
        NonResident = 2,

        /// <summary>
        /// Высококвалифицированный специалист, не являющийся налоговым резидентом России
        /// </summary>
        QualifiedNonResident = 3,

        /// <summary>
        /// нерезидент/беженец или нерезидент/получено временное убежище
        /// </summary>
        RefugeeNonResident = 5,

        /// <summary>
        /// патент (вне зависимости от того резидент или нерезидент)
        /// </summary>
        Patent = 6,

        /// <summary>
        /// Высококвалифицированный специалист, являющийся налоговым резидентом России
        /// </summary>
        QualifiedResident = 7,
    }
}