namespace Moedelo.CommonV2.WhiteLabel.Services;

public interface ISettingWrapService
{
    string GetWlHostPattern();

    string GetSberWlHostPattern();

    string GetEnvironment();
}