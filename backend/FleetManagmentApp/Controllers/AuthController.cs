using FleetManagmentApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FleetManagmentApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;


        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            Console.WriteLine("Email: " + model.Email);
            Console.WriteLine("Password: " + model.Password);

            if (!model.Email.Contains("@"))
            {
                return BadRequest(new { message = "Nieprawidłowy adres e-mail. Musi zawierać znak '@'." });
            }

            if (!model.Password.Any(char.IsUpper))
            {
                return BadRequest(new { message = "Hasło musi zawierać co najmniej jedną dużą literę." });
            }
            if (model.Password.Length < 8)
            {
                return BadRequest(new { message = "Hasło musi mieć co najmniej 8 znaków." });
            }
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                return BadRequest(new { message = "Użytkownik o podanym e-mailu już istnieje." });
            }

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok(new { message = "Konto zostało pomyślnie utworzone." });
            }

            var errors = result.Errors.Select(e => e.Description).ToList();
            return BadRequest(new { message = "Rejestracja nieudana", errors });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return Unauthorized(new { message = "Taki użytkownik nie istnieje." });
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return Ok(new { message = "Zalogowano pomyślnie." });
            }

            if (result.IsLockedOut)
            {
                return Unauthorized(new { message = "Konto zostało zablokowane. Spróbuj później." });
            }

            return Unauthorized(new { message = "Nieprawidłowy e-mail lub hasło." });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpGet("check")]
        public IActionResult Check()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Ok();
            }
            return Unauthorized();
        }
    }
}
