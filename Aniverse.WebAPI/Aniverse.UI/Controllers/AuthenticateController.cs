using Aniverse.Business.DTO_s.Authentication;
using Aniverse.Business.Helpers;
using Aniverse.Business.Services.Interface;
using Aniverse.Core.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Aniverse.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtService _jwtService;
        public AuthenticateController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
        }
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] Register register)
        {
            AppUser isEmailExsist = await _userManager.FindByNameAsync(register.Email);
            if (isEmailExsist != null)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new { status = "error", message = "Email is already exisit" });
            }
            AppUser user = new AppUser
            {
                Firstname = register.Firstname,
                Lastname = register.Lastname,
                Email = register.Email,
                UserName = register.Username,
                Gender = register.Gender,
            };
            IdentityResult result = await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    return StatusCode(StatusCodes.Status403Forbidden, new { status = error.Code, message = error.Description });
                }
            };
            await _userManager.AddToRoleAsync(user, Roles.Member.ToString());
            return Ok(new { statsu = "Success", message = "Confirmation email sent" });
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] Login login)
        {
            AppUser user = await _userManager.FindByNameAsync(login.Username);
            if (user == null) return NotFound();
            if (!await _userManager.CheckPasswordAsync(user, login.Password)) return Unauthorized();
            var roles = _userManager.GetRolesAsync(user).Result;
            return Ok(_jwtService.GetJwt(user, roles));
        }
        #region CreateRoles
        //[HttpPost("createroles")]
        //public async Task CreateRoles()
        //{
        //    foreach (var item in Enum.GetValues(typeof(Roles)))
        //    {
        //        if (!(await _roleManager.RoleExistsAsync(item.ToString())))
        //        {
        //            await _roleManager.CreateAsync(new IdentityRole
        //            {
        //                Name = item.ToString()
        //            });
        //        }
        //    }
        //}
        #endregion
    }
}
