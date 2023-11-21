using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Services.Authentication;

public interface ITokenService
{
    string CreateToken(IdentityUser user, string role);
}