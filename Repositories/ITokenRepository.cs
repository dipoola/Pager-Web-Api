using Microsoft.AspNetCore.Identity;
using System.Security;

namespace Pager.Repositories
{
    public interface ITokenRepository
    {

       string  CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
