using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Widely.Models;
using Widely.CommonMethod;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Security.Claims;

namespace Widely.Controllers
{
    public class Account : Controller
    {
        private readonly DB_ATMContext _context;
        public Account(DB_ATMContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(tbl_User _User)
        {
            string E_mail = _User.Email;
            string Password = _User.LoginPassword;
            var user = await _context.tbl_User.Where(x => x.Email == E_mail).FirstOrDefaultAsync(); //checking if the emailid already exits for any user
            ModelState.Remove("FirstName");
            ModelState.Remove("LastName");
            ModelState.Remove("Phone");
            ModelState.Remove("DOB");
            ModelState.Remove("Balance");
            if (ModelState.IsValid)
            {
                if (user is not null)
                {
                    if (user.LoginPassword == PasswordEncrypt.EncodePasswordToBase64(Password))
                    {
                        Global_Variables.UserLogin = user.FirstName + " " + user.LastName;
                        Global_Variables.LoginID = user.UserID;
                        return View("Welcome");

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid Password");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Incorrect Email");
                }
            }

            return View();
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(tbl_User _User)
        {
            string E_mail = _User.Email;
            string Password = _User.LoginPassword;
            _User.LoginPassword = PasswordEncrypt.EncodePasswordToBase64(Password);
            var userWithSameEmail = _context.tbl_User.Where(x => x.Email == E_mail).FirstOrDefault(); //checking if the emailid already exits for any user
            if (userWithSameEmail != null)
            {
                ModelState.AddModelError(string.Empty, "User with this Email Already Exist");
            }

            else
            {
                ModelState.Remove("Balance");
                if (ModelState.IsValid)
                {
                    if (userWithSameEmail == null)
                    {

                        _context.tbl_User.Add(_User);
                        try
                        {
                            _context.SaveChanges();
                        }
                        catch (NullReferenceException ex)
                        {
                            ViewBag.Message = "Something Went Wrong Please Try Again";
                            return View();
                        }

                        ModelState.AddModelError(string.Empty, "Registration Successful");
                    }
                }
            }
            return View("Login");

        }

        public async Task<IActionResult> ChangePass()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePass(tbl_User _User, string password)
        {
            string Password = _User.LoginPassword;
            string NP = password;
            var user = await _context.tbl_User.FirstOrDefaultAsync(m => m.UserID == Global_Variables.LoginID); //checking if the emailid already exits for any user
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Something Went Wrong Please Try Again");
            }
            else
            {
                ModelState.Remove("FirstName");
                ModelState.Remove("LastName");
                ModelState.Remove("Email");
                ModelState.Remove("Phone");
                ModelState.Remove("DOB");
                ModelState.Remove("Balance");
                if (ModelState.IsValid)
                {
                    if (user.LoginPassword == PasswordEncrypt.EncodePasswordToBase64(Password))
                    {
                        if (user.LoginPassword != PasswordEncrypt.EncodePasswordToBase64(password))
                        {
                            user.LoginPassword = PasswordEncrypt.EncodePasswordToBase64(password);
                            try
                            {
                                _context.SaveChanges();
                            }
                            catch (NullReferenceException ex)
                            {
                                ViewBag.Message = "Something Went Wrong Please Try Again";
                                return View();
                            }
                            ModelState.AddModelError(string.Empty, "Password Changed");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "New Password Can't Be Your Current Password");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid Password");
                    }
                }
            }
            return View();
        }
        public IActionResult Logout()
        {
            Global_Variables.UserLogin = null;
            Global_Variables.LoginID = 0;
            return Redirect("https://localhost:5001/Account/Login");

        }
    }
}
