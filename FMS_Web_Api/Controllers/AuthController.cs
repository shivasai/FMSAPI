using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FMS_Web_Api.Configuration;
using FMS_Web_Api.DAL;
using FMS_Web_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FMS_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtBearerTokenSettings jwtBearerTokenSettings;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ApplicationDbContext _dbContext;
        public AuthController(IOptions<JwtBearerTokenSettings> jwtTokenOptions, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext dbContext)
        {
            this.jwtBearerTokenSettings = jwtTokenOptions.Value;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this._dbContext = dbContext;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserDetails userDetails)
        {
            if (!ModelState.IsValid || userDetails == null)
            {
                return new BadRequestObjectResult(new { Message = "User Registration Failed" });
            }

            var identityUser = new IdentityUser() { UserName = userDetails.UserName, Email = userDetails.Email };
            var result = await userManager.CreateAsync(identityUser, userDetails.Password);
            if (!result.Succeeded)
            {
                var dictionary = new ModelStateDictionary();
                foreach (IdentityError error in result.Errors)
                {
                    dictionary.AddModelError(error.Code, error.Description);
                }

                return new BadRequestObjectResult(new { Message = "User Registration Failed", Errors = dictionary });
            }

            return Ok(new { Message = "User Reigstration Successful" });
        }
        [HttpGet]
        [Route("PMOUsers")]
        public async Task<List<IdentityUser>> GetPMOUsers()
        {
            List<IdentityUser> pmoUsers = new List<IdentityUser>();
            var users = this._dbContext.Users.ToList();
            foreach(var user in users)
            {
                var roles = await userManager.GetRolesAsync(user);
                if(roles.Contains("PMO"))
                {
                    pmoUsers.Add(user);
                }
            }
            return pmoUsers;
        }
        [HttpPost]
        [Route("RemovePmoUser")]
        public async Task<ActionResult> RemovePmoUser([FromBody] PMOUser userdetails)
        {
            string username = userdetails.email.Substring(0, userdetails.email.IndexOf("@"));
            var user = await userManager.FindByNameAsync(username);
            if(user == null)
            {
                return NotFound();
            }
           await userManager.RemoveFromRoleAsync(user, "PMO");
            return Ok();
        }
        [HttpPost]
        [Route("RegisterPMO")]
        public async Task<IActionResult> RegisterPMO([FromBody] PMOUser userdetails)
        {
            string email = userdetails.email;
            if (!ModelState.IsValid || email == null)
            {
                return new BadRequestObjectResult(new { Message = "User Registration Failed" });
            }
            string username = email.Substring(0, email.IndexOf("@"));
            var identityUser = new IdentityUser() { UserName = username, Email = email };
            string password = "PmoUser@123";
            var result = await userManager.CreateAsync(identityUser, password);
            if (!result.Succeeded)
            {
                var dictionary = new ModelStateDictionary();
                foreach (IdentityError error in result.Errors)
                {
                    dictionary.AddModelError(error.Code, error.Description);
                }

                return new BadRequestObjectResult(new { Message = "User Registration Failed", Errors = dictionary });
            }

            await userManager.AddToRoleAsync(identityUser, "PMO");  
            return Ok(new { Message = "User Reigstration Successful" });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginCredentials credentials)
        {
            IdentityUser identityUser;

            if (!ModelState.IsValid
                || credentials == null
                || (identityUser = await ValidateUser(credentials)) == null)
            {
                return new BadRequestObjectResult(new { Message = "Login failed" });
            }
            string role = await GetUserRole(credentials);
            var token = GenerateToken(identityUser);
            return Ok(new { token = token, username = identityUser.UserName, role = role });
        }

        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            // Well, What do you want to do here ?
            // Wait for token to get expired OR 
            // Maintain token cache and invalidate the tokens after logout method is called
            return Ok(new { Token = "", Message = "Logged Out" });
        }

        private async Task<string> GetUserRole(LoginCredentials credentials)
        {
            

            var identityUser = await userManager.FindByNameAsync(credentials.Username);
            if (identityUser != null)
            {
                var role = await userManager.GetRolesAsync(identityUser);

                return role.FirstOrDefault();
            }

            return null;
        }
        private async Task<IdentityUser> ValidateUser(LoginCredentials credentials)
        {
            var identityUser = await userManager.FindByNameAsync(credentials.Username);
            if (identityUser != null)
            {
                var result = userManager.PasswordHasher.VerifyHashedPassword(identityUser, identityUser.PasswordHash, credentials.Password);
                return result == PasswordVerificationResult.Failed ? null : identityUser;
            }

            return null;
        }


        private object GenerateToken(IdentityUser identityUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtBearerTokenSettings.SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, identityUser.UserName.ToString()),
                    new Claim(ClaimTypes.Email, identityUser.Email)
                }),

                Expires = DateTime.UtcNow.AddSeconds(jwtBearerTokenSettings.ExpiryTimeInSeconds),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = jwtBearerTokenSettings.Audience,
                Issuer = jwtBearerTokenSettings.Issuer
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
