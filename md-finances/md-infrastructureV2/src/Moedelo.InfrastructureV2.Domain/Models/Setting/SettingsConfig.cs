namespace Moedelo.InfrastructureV2.Domain.Models.Setting;

public record SettingsConfig(
    string Environment,
    string Shard,
    string ConfigName,
    string Domain,
    string AppName);
