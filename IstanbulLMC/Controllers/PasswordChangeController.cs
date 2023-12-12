using IstanbulLMC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Net.Mail;


//using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using IstanbulLMC.ViewModel;

namespace IstanbulLMC.Controllers
{
    [AllowAnonymous]

    public class PasswordChangeController : Controller
    {
        private readonly UserManager<AppUser> _userM;
        private readonly ILogger<PasswordChangeController> _logger;

        public PasswordChangeController(UserManager<AppUser> userM, ILogger<PasswordChangeController> logger)
        {
            _userM = userM;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPass forgotPass)
        {
            try
            {
                var user = await _userM.FindByEmailAsync(forgotPass.Mail);
                if (user == null)
                {

                    TempData["Error"] = "This email address is not in use";
                    return View(forgotPass); // You can create a user not found view
                }

                string passwordResetToken = await _userM.GeneratePasswordResetTokenAsync(user);
                var passwordResetTokenLink = Url.Action("ResetPassword", "PasswordChange", new
                {
                    userId = user.Id,
                    token = passwordResetToken
                }, HttpContext.Request.Scheme);

                string emailBody = $@"<!DOCTYPE html>
<html>
<head>
    <style>
        body {{
            font-family: Arial, sans-serif;
            text-align:center;
        }}
        .link-button {{
            display: inline-block;
            background-color: #c29d59;
            color: #fff;
            padding: 10px 20px;
            text-align: center;
            text-decoration: none;
            border-radius: 5px;
        }}
    </style>
</head>
<body>
    <p>Hello {user.UserName},</p>
    <p>A request has been received to change the password for your Millennium account.</p>
    <p>If you initiated this request, please click the button below to reset your password:</p>
    <a class=""link-button"" href=""{passwordResetTokenLink}"">Reset Password</a>
    <p>If you did not initiate this request, please contact us immediately at <a href=""mailto:support@millenniumtraveltr.com"">support@millenniumtraveltr.com</a></p>
    <p>Thank you,</p>
    <p>The MillenniumTravelTR Team</p>
</body>
</html>";

                MimeMessage mimeMessage = new MimeMessage();
                MailboxAddress mailboxAddressFrom = new MailboxAddress("The MillenniumTravelTR Team", "resetPassword@millenniumtraveltr.com");
                mimeMessage.From.Add(mailboxAddressFrom);
                MailboxAddress mailboxAddressTo = new MailboxAddress("username", forgotPass.Mail);
                mimeMessage.To.Add(mailboxAddressTo);

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = emailBody;
                mimeMessage.Body = bodyBuilder.ToMessageBody();
                mimeMessage.Subject = "Forgot Password";

                //using (SmtpClient client = new SmtpClient())
                //{
                //    client.Connect("mt-odin-win.guzelhosting.com", 465, true);
                //    client.Authenticate("resetPassword@millenniumtraveltr.com", "millenniuumPassword");
                //    client.Send(mimeMessage);
                //    client.Disconnect(true);
                //}
                TempData["Success"] = "Email has been sent.";
                return View(); // You can create a success view
            }
            catch (Exception ex)
            {
                // Log the error
                _logger.LogError(ex, "Error sending email.");

                // Handle the error, you can redirect to an error page or show an error message
                TempData["error"] = "an error occured while trying to send the email. Error details:\n";
                return View(); // You can create an email error view
            }
        }


        [HttpGet]
        public IActionResult ResetPassword(string userId, string token)
        {

            TempData["userId"] = userId;
            TempData["token"] = token;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPass resetPass)
        {

            var userId = TempData["userId"];
            var token = TempData["token"];
            if (userId == null || token == null)
            {

                ///hATA mesaji

            }
            var user = await _userM.FindByIdAsync(userId.ToString());
            var result = await _userM.ResetPasswordAsync(user, token.ToString(), resetPass.Pass);
            if (result.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
    }
}
