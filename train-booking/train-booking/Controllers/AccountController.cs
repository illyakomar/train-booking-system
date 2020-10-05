using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using train_booking.ViewModels.Account;
using train_booking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using train_booking.Services.Interfaces;
using System.Security.Claims;

namespace train_booking.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<User> _signInManager;
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private IUsersRepository _userRepository;
        private IEmailSender _emailSender;

        public AccountController
        (
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IUsersRepository userRepository,
            IEmailSender emailSender
        )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _userRepository = userRepository;
            _emailSender = emailSender;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null, string error = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Profile");
            }

            if (error != null)
            {
                ModelState.AddModelError("", error);
            }
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    await _userRepository.GetUserByLogin(model.Email);
                    return RedirectToAction("Index", "Profile");
                }
                ModelState.AddModelError("", "Неправильний логін або пароль");
            }
            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Profile");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (await _userRepository.IsEmailUnique(viewModel.Email))
                {
                    User user = new User
                    {
                        Email = viewModel.Email,
                        UserName = viewModel.Email,
                        LastName = viewModel.LastName,
                        FirstName = viewModel.FirstName,
                        MiddleName = viewModel.MiddleName,
                        Passport = viewModel.Passport
                    };

                    var createUserResult = await _userManager.CreateAsync(user, viewModel.Password);

                    if (createUserResult.Succeeded)
                    {
                        if (!await _roleManager.RoleExistsAsync("Passenger"))
                        {
                            await _roleManager.CreateAsync(new IdentityRole("Administrator"));
                            await _roleManager.CreateAsync(new IdentityRole("Dispatcher"));
                            await _roleManager.CreateAsync(new IdentityRole("Passenger"));
                        }

                        if (viewModel.Role == "Administrator")
                        {
                            await _userManager.AddToRoleAsync(user, "Administrator");
                        }
                        else if (viewModel.Role == "Dispatcher" && User.IsInRole("Administrator"))
                        {
                            await _userManager.AddToRoleAsync(user, "Dispatcher");
                        }
                        else
                        {
                            await _userManager.AddToRoleAsync(user, "Passenger");
                        }

                        if (!string.IsNullOrWhiteSpace(user.Email))
                        {
                            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, user.Email));
                        }

                        if (!User.IsInRole("Administrator"))
                        {
                            var signInUserResult = await _signInManager.PasswordSignInAsync(viewModel.Email, viewModel.Password, true, false);

                            if (signInUserResult.Succeeded)
                            {
                                await _userManager.FindByIdAsync(user.Id);
                                return RedirectToAction("RegisterSuccess", "Account");
                            }
                        }
                        else
                        {
                            return Ok();
                        }
                    }
                    else
                    {
                        foreach (var error in createUserResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Ця пошта вже зареєстрована");
                }
            }

            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            var result = await _userManager.ChangePasswordAsync(await _userManager.GetUserAsync(User), model.OldPassword, model.NewPassword);

            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordSuccess", "Account");
            }

            return Problem();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    if (user.ResetCode == model.Code)
                    {
                        var result = await _userManager.RemovePasswordAsync(user);

                        if (result.Succeeded)
                        {
                            result = await _userManager.AddPasswordAsync(user, model.NewPassword);

                            if (result.Succeeded)
                            {
                                return RedirectToAction("ResetPasswordSuccess", "Account");
                            }
                        }
                    }
                }
                else
                {
                    return RedirectToAction("ForgotPassword", new { error = "Користувач з таким email не знайдений." });
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RequestResetPassword(SubmitEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    string resetCode = GenerateRandomKey();

                    await _userRepository.AddResetCodeForUser(user, resetCode);
                    await _emailSender.SendEmailAsync(user.Email, "Відновлення паролю", user.UserName, resetCode);

                    return Ok();
                }

                return NotFound();
            }

            return RedirectToAction("ForgotPassword", new { error = "Виникла проблема із посиланням, спробуйте ще раз." });
        }

        [HttpPost]
        public async Task<IActionResult> VerifyResetCode(VerifyResetCodeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    if (user.ResetCode == model.ResetCode)
                    {
                        if (user.LastResetCodeCreationTime.AddMinutes(5) > DateTime.Now)
                        {
                            return Ok();
                        }

                        await _userRepository.ClearResetCodeForUser(user);

                        return Problem("Ваш код вже недійсний");
                    }

                    return Problem("Ви ввели недійсний код");
                }

                return NotFound();
            }

            return Problem();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserRole()
        {
            string roleName = (await _userManager.GetRolesAsync(await _userManager.GetUserAsync(User))).FirstOrDefault();

            if (roleName == null)
            {
                return NotFound();
            }

            return Ok(roleName);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAuthorizedUserInfo()
        {
            var user = await _userManager.GetUserAsync(User);

            return Json(new UserInfoViewModel { Email = user.Email, Login = user.UserName });
        }

        [HttpGet]
        public IActionResult CheckIfUserAlreadyAuthorized()
        {
            return Json(User.Identity.IsAuthenticated);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserId()
        {
            return Json((await _userManager.GetUserAsync(User)).Id);
        }

        private string GenerateRandomKey()
        {
            Random random = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            int length = 8;

            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
