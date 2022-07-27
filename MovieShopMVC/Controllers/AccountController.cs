﻿using ApplicationCore.Models;
using ApplicationCore.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MovieShopMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // account/register
        [HttpGet]
        public IActionResult Register()
        {
            // show the View so that user can enter info and click on register button
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            // service, hash the password and save in database
            var user = await _accountService.CreateUser(model);
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            var user = await _accountService.ValidateUser(model);
            if (user == false)
            {
                ModelState.AddModelError( "" , "Password is invalid");
                return View(model);
            }
            return LocalRedirect("~/");
        }
    }
}