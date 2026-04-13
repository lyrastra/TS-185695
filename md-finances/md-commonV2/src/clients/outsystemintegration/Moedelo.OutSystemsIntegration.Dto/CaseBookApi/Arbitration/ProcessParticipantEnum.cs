namespace Moedelo.OutSystemsIntegrationV2.Dto.CaseBookApi.Arbitration
{
    public enum ProcessParticipantEnum
    {
        /// <summary>
        /// Любой
        /// </summary>
        Any = -1,

        /// <summary>
        ///  Истец
        /// </summary>
        Claimant = 0,

        /// <summary>
        /// Ответчик
        /// </summary>
        Defendant = 1,

        /// <summary>
        /// Третье лицо
        /// </summary>
        ThirdParty = 2,

        /// <summary>
        /// Иное лицо
        /// </summary>
        Other = 3
    }
}
