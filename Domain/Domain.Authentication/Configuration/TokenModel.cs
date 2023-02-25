namespace Domain.Authentication.Configuration;

public class TokenModel
{
    public string? AccessToken { get; }
    public DateTime? AcessTokenExpiration { get; }
    public string? RefreshToken { get; }
    public DateTime? RefreshTokenExpiration { get; }

    public TokenModel(string? accessToken, DateTime? acessTokenExpiration, string? refreshToken, DateTime? refreshTokenExpiration)
    {
        AccessToken = accessToken;
        AcessTokenExpiration = acessTokenExpiration;
        RefreshToken = refreshToken;
        RefreshTokenExpiration = refreshTokenExpiration;
    }
}