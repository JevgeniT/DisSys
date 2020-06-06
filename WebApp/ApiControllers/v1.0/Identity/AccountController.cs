using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Identity;
using Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Public.DTO;
using Public.DTO.Identity;

namespace WebApp.ApiControllers._1._0.Identity
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;


        public AccountController(IConfiguration configuration, UserManager<AppUser> userManager,
            ILogger<AccountController> logger, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _logger = logger;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Login([FromBody] LoginDTO model)
        {

             var appUser = await _userManager.FindByEmailAsync(model.Email);

             Console.WriteLine(appUser.Id + " Db GUID" );
             
            if (appUser == null)
            {
                _logger.LogInformation($"Web-Api login. User {model.Email} not found!");
                return StatusCode(403);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(appUser, model.Password, false);
            if (result.Succeeded)
            {
                var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser); //get the User analog
                var jwt = IdentityExtensions.GenerateJWT(claimsPrincipal.Claims,
                    _configuration["JWT:SigningKey"],
                    _configuration["JWT:Issuer"],
                    _configuration.GetValue<int>("JWT:ExpirationInDays")
                );
                _logger.LogInformation($"Token generated for user {model.Email} ");
                return Ok(new {token = jwt, status = "Logged in"});
            }

            _logger.LogInformation($"Web-Api login. User {model.Email} attempted to log-in with bad password!");
            return StatusCode(403);
        }


        [HttpPost]
        public async Task<ActionResult<string>> Register([FromBody] RegisterDTO dto)
        {
            var appUser = await _userManager.FindByEmailAsync(dto.Email);
            if (appUser != null)
            {
                _logger.LogInformation($"WebApi register. User {dto.Email} already registered!");
                return NotFound(new MessageDTO("User already registered!"));
            }
            
            appUser = new AppUser()
            {
                Email = dto.Email,
                UserName = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
            };

            var role = dto.IsHost ? "host" : "guest";
          
            
            var result = await _userManager.CreateAsync(appUser, dto.Password);
            
            if (result.Succeeded)
            {
                _logger.LogInformation($"User {appUser.Email} created a new account as {role}.");
                await _userManager.AddToRoleAsync(appUser, role);
                
                var user = await _userManager.FindByEmailAsync(appUser.Email);
                if (user != null)
                {
                    var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);
                    var jwt = IdentityExtensions.GenerateJWT(
                        claimsPrincipal.Claims
                            .Append(new Claim(ClaimTypes.GivenName, appUser.FirstName))
                            .Append(new Claim(ClaimTypes.Surname, appUser.LastName)),
                        _configuration["JWT:SigningKey"],
                        _configuration["JWT:Issuer"],
                        _configuration.GetValue<int>("JWT:ExpirationInDays")
                    );
                    _logger.LogInformation($"WebApi register. User {user.Email} logged in.");
                    return Ok(new JwtResponseDTO()
                    {
                        Token = jwt, Status = $"User {user.Email} created and logged in.",
                        FirstName = appUser.FirstName, LastName = appUser.LastName
                    });
                }

                _logger.LogInformation($"User {appUser.Email} not found after creation!");
                return BadRequest(new MessageDTO("User not found after creation!"));
            }

            var errors = result.Errors.Select(error => error.Description).ToList();
            return BadRequest(new MessageDTO() {Messages = errors});

        }

        
 
    }
}