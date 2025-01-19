using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Widely.Models;
using Widely.CommonMethod;
using System.Web;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.InteropServices;
using System.IO;
using System.Text;

namespace Widely.Controllers
{
    public class NavOption : Controller
    {

        private readonly DB_ATMContext _context;

        public NavOption(DB_ATMContext context)
        {
            _context = context;
        }

        //Your Account
        public async Task<IActionResult> YourAcc()
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            var Tbl_User = await _context.tbl_User
                .FirstOrDefaultAsync(m => m.UserID == Global_Variables.LoginID);
            if (Tbl_User == null)
            {
                return NotFound();
            }

            return View(Tbl_User);
        }

        //Deposit
        [HttpGet]
        public async Task<IActionResult> Deposit()
        {
            var Tbl_User = await _context.tbl_User
                .FirstOrDefaultAsync(m => m.UserID == Global_Variables.LoginID);
            if (Tbl_User == null)
            {
                return NotFound();
            }

            return View(Tbl_User);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deposit(tbl_User _User)
        {
            string Password = _User.LoginPassword;
            int Balance = _User.Balance;
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
                if (ModelState.IsValid)
                {
                    if (user.LoginPassword == PasswordEncrypt.EncodePasswordToBase64(Password))
                    {
                        user.Balance = Balance + user.Balance;
                        try
                        {
                            _context.SaveChanges();
                        }
                        catch (NullReferenceException ex)
                        {
                            ViewBag.Message = "Something Went Wrong Please Try Again";
                            return View();
                        }
                        ModelState.AddModelError(string.Empty,Balance + " " + "PKR Has Been Added To Your Account");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid Password");
                    }
                }
            }
            return View(user);
        }

        //Withdraw
        [HttpGet]
        public async Task<IActionResult> Withdraw()
        {
            var Tbl_User = await _context.tbl_User
                .FirstOrDefaultAsync(m => m.UserID == Global_Variables.LoginID);
            if (Tbl_User == null)
            {
                return NotFound();
            }

            return View(Tbl_User);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Withdraw(tbl_User _User,string name)
        {
            string Password = _User.LoginPassword;
            int Balance = _User.Balance;
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
                if (ModelState.IsValid)
                {
                    if (user.LoginPassword == PasswordEncrypt.EncodePasswordToBase64(Password))
                    {
                        if (user.Balance >= Balance)
                        {
                            user.Balance = user.Balance - Balance;
                            try
                            {
                               _context.SaveChanges();
                            }
                            catch (NullReferenceException ex)
                            {
                                ViewBag.Message = "Something Went Wrong Please Try Again";
                                return View();
                            }
                            ModelState.AddModelError(string.Empty, Balance + " " + "PKR Has Been Deducted From Your Account");
                       
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Not Enough Amount");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid Password");
                    }
                }
            }
            return View(user);           
        }

        //Transaction
        [HttpGet]
        public async Task<IActionResult> Transaction()
        {
            var Tbl_User = await _context.tbl_User
                .FirstOrDefaultAsync(m => m.UserID == Global_Variables.LoginID);
            if (Tbl_User == null)
            {
                return NotFound();
            }

            return View(Tbl_User);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Transaction(tbl_User _User, int TransactionID, int TransactionAmt)
        {
            string Password = _User.LoginPassword;
            var user = await _context.tbl_User.FirstOrDefaultAsync(m => m.UserID == Global_Variables.LoginID); //checking if the emailid already exits for any user
            var TransID = await _context.tbl_User.FindAsync(TransactionID);
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
                        if (user.Balance >= TransactionAmt)
                        {
                            if (TransID != null)
                            {
                                if (user.UserID != TransID.UserID)
                                {
                                    user.Balance = user.Balance - TransactionAmt;
                                    TransID.Balance = TransID.Balance + TransactionAmt;
                                    try
                                    {
                                        _context.SaveChanges();
                                    }
                                    catch (NullReferenceException ex)
                                    {
                                        ViewBag.Message = "Something Went Wrong Please Try Again";
                                        return View();
                                    }
                                    ModelState.AddModelError(string.Empty, TransactionAmt + " " + "PKR Has Been Transfered To" + " " + TransID.FirstName + " " + TransID.LastName);
                                }
                                else
                                {
                                    ModelState.AddModelError(string.Empty, "Not Possible");
                                }
                            }
                            else
                            {
                                ModelState.AddModelError(string.Empty, "Invalid Transaction ID");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Not Enough Amount");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid Password");
                    }
                }
            }
            return View(user);
        }
    }
}
