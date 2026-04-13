namespace Moedelo.Common.Settings.Abstractions.Models
{
    public sealed class SettingsConfig
    {
        public string Environment { get; init; }

        public string Shard { get; init; }

        public string ConfigName { get; init; }

        public string Domain { get; init; }

        public string AppName { get; init; }

        public string AuditTrailAppName { get; init; }
    }
}
