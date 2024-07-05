using Microsoft.AspNetCore.Identity;

namespace Kkmlsfi_ns.API.Repository.Interface
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
