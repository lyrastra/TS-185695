namespace Moedelo.CommonV2.Auth.Domain;

public interface IUserFirmContextInitializer
{
    void InitializeContext(AuthenticationInfo authenticationInfo);
}