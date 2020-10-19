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
using Microsoft.AspNetCore.Routing;

namespace train_booking.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUsersRepository _userRepository;
        private readonly IEmailSender _emailSender;
        private readonly IHttpContextAccessor _httpContext;
        private readonly LinkGenerator _urlHelper;

        public AccountController
        (
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IUsersRepository userRepository,
            IHttpContextAccessor httpContext,
            LinkGenerator urlHelper,
            IEmailSender emailSender
        )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _userRepository = userRepository;
            _emailSender = emailSender;
            _httpContext = httpContext;
            _urlHelper = urlHelper;
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
                    return RedirectToAction("CheckerRole", "Home");
                }
                ModelState.AddModelError("", "Неправильний логін або пароль");
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
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
                        await _userManager.AddToRoleAsync(user, "Passenger");

                        if (!string.IsNullOrWhiteSpace(user.Email))
                        {
                            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, user.Email));
                        }

                        var signInUserResult = await _signInManager.PasswordSignInAsync(viewModel.Email, viewModel.Password, true, false);

                        if (signInUserResult.Succeeded)
                        {
                            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                            var callbackUrl = _urlHelper.GetUriByAction(_httpContext.HttpContext, "ConfirmEmail", "Account", new { userId = user.Id, code, email = user.Email });

                            await _emailSender.SendDefaultEmailAsync(user.Email, "Підтвердження аккаунту", $"Перейдіть за посиланням:", callbackUrl, "Активувати!");

                            await _userManager.FindByIdAsync(user.Id);
                            return RedirectToAction("RegisterSuccess", "Account");
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

            return RedirectToAction("Index", "Home", viewModel);
        }

       
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string code, string email)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
            {
                await _emailSender.SendDefaultEmailAsync(email, "Реєстрація", $"Аккаунт упішно підтверджений!", $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToString()}/", "Перейти до системи");
            }

            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        [HttpGet]
        public IActionResult CheckValidationCode(string email)
        {
            ViewBag.Email = email;
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword(string error = "")
        {
            ViewBag.Error = error;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return RedirectToAction("ForgotPassword", new { error = "Користувач з таким email не знайдений." });
                }
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);

                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code, email = user.Email }, protocol: HttpContext.Request.Scheme);
                await _emailSender.SendDefaultEmailAsync(model.Email, "Відновлення паролю", "Щоб відновити пароль перейдіть за посиланням:", callbackUrl, "Відновити!");

                return View("ForgotPasswordSuccess");
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string email, string code = null)
        {
            if (code == null)
            {
                return RedirectToAction("ForgotPassword", new { error = "Виникла проблема із посиланням, спробуйте ще раз." });
            }

            var model = new ResetPasswordViewModel { Code = code, Email = email };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model, string code = null)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            model.Email = user.Email;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (user == null)
            {
                return RedirectToAction("ForgotPassword", new { error = "Користувач з таким email не знайдений." });
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);

            if (result.Succeeded)
            {
                await _emailSender.SendDefaultEmailAsync(model.Email, "Відновлення паролю", $"Ви успішно змінили свій пароль!", $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToString()}/", "Перейти до системи");
                return RedirectToAction("ResetPasswordSuccess", "Account");
            }

            ViewBag.Errors = result.Errors.Select(x => x.Description).ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult ForgotPasswordSuccess()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPasswordSuccess()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RegisterSuccess()
        {
            return View();
        }

    }
}
