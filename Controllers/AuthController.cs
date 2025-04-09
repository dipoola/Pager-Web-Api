using Azure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pager.DTOs;
using Pager.Repositories;

namespace Pager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }
        //POST: API /Auth/ REGISTER
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            var Identityuser = new IdentityUser

            {
                UserName = registerRequestDTO.Username,
                Email = registerRequestDTO.Username,
            };

            var IdentityResult = await userManager.CreateAsync(Identityuser, registerRequestDTO.Password);

            if (IdentityResult.Succeeded)
            {
                //Add roles to user

                if (registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
                {
                    IdentityResult = await userManager.AddToRolesAsync(Identityuser, registerRequestDTO.Roles);

                    if (IdentityResult.Succeeded)
                    {
                        return Ok("User was registered!  pls login .");
                    }

                }


            }
            return BadRequest("Sommething went wrong!");

        }

        //POST : Api / Auth / LOGIN
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDTO.Username);

            if (user != null)
            {
                var checkpassowrd = await userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

                if (checkpassowrd)
                {

                    //get roles for this user 

                    var roles = await userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        //create token

                        var JwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());

                        var response = new LoginResponseDTO
                        {
                            JwtToken = JwtToken,
                        };
                        return Ok(response);
                    }

                }
            }

            return BadRequest("Usernme or password incorrect");
        }
    }

}
