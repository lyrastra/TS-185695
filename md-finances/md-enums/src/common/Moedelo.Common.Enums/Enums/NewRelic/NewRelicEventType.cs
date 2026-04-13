namespace Moedelo.Common.Enums.Enums.NewRelic
{
    public enum NewRelicEventType : byte
    {
        None = 0,

        /// <summary> Событие началось </summary>
        Open = 1,

        /// <summary> Событие завершилось </summary>
        Closed = 2
    }
}