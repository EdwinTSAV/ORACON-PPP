using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Oracon.Models;
using Oracon.Models.Class;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace Oracon.Controllers
{
    public class HomeController : Controller
    {
        private readonly string email = "juliaRAF2020@gmail.com";
        private readonly string password = "juliaR4F2O2O";

        private readonly ILogger<HomeController> _logger;
        private readonly Oracon_Context context;
        private readonly IConfiguration configuration;

        public HomeController(ILogger<HomeController> logger, Oracon_Context context, IConfiguration configuration)
        {
            _logger = logger;
            this.context = context;
            this.configuration = configuration;
        }
        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var users = context.Usuarios.Where(o => o.Id == GetLoggedUser().Id)
                    .FirstOrDefault();

                ViewBag.User = users;

                if (!users.CorreoConf)
                {
                    Random random = new Random();
                    int numero = random.Next(100000, 999999);
                    var confirmacion = CreateHash(numero.ToString());

                    users.Recovery = confirmacion;
                    context.Usuarios.Update(users);
                    context.SaveChanges();

                    EmailSend(users.Correo, numero);
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Confirm(int confirm)
        {
            var user = context.Usuarios.Where(o => o.Id == GetLoggedUser().Id)
                    .FirstOrDefault();

            var confirmacion = CreateHash(confirm.ToString());

            if (confirmacion == user.Recovery)
            {
                user.CorreoConf = true;
                user.Recovery = null;
                context.Usuarios.Update(user);
                context.SaveChanges();
            }
            else
            {
                ModelState.AddModelError("confirm", "Código inválido");
            }
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        protected Usuario GetLoggedUser()
        {
            var claim = HttpContext.User.Claims.FirstOrDefault();
            var user = context.Usuarios.Where(o => o.Username == claim.Value).FirstOrDefault();
            return user;
        }
        protected void EmailSend(string emailUser, int correo)
        {
            MailMessage mailMessage = new MailMessage(email, emailUser,
                "Verificación del correo electrónico de ORACON.SRL",
                "Codigo de verificación de la cuenta <strong>" + correo + "</strong> no compartas este código con nadie.")
            {
                IsBodyHtml = true
            };
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Host = "smtp.gmail.com",
                Port = 587,
                Credentials = new System.Net.NetworkCredential(email, password)
            };
            smtpClient.Send(mailMessage);
            smtpClient.Dispose();
        }

        private string CreateHash(string input)
        {
            input += configuration.GetValue<string>("Token");
            var sha = SHA512.Create();
            var bytes = Encoding.Default.GetBytes(input);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
