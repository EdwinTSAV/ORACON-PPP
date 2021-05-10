using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Oracon.Models;
using Oracon.Models.Class;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Oracon.Controllers
{
    public class UserController : Controller
    {
        private readonly string email = "juliaRAF2020@gmail.com";
        private readonly string password = "juliaR4F2O2O";
        private readonly string urlDomain = "https://localhost:44363/";
        private readonly Oracon_Context context;
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment hosting;
        public UserController(Oracon_Context context, IConfiguration configuration, IWebHostEnvironment hosting)
        {
            this.context = context;
            this.configuration = configuration;
            this.hosting = hosting;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = context.Usuarios
                .Where(o => o.Username == username && o.Password == CreateHash(password))
                .FirstOrDefault();

            if (user != null)
            {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, username)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                HttpContext.SignInAsync(claimsPrincipal);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                HttpContext.Response.StatusCode = 400;
                ModelState.AddModelError("Login", "Usuario o contraseña incorrectos");
                return View();
            }
        }

        [HttpGet]
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new Usuario());
        }

        [HttpPost]
        public IActionResult Register(Usuario usuario, IFormFile perfil, string passwordConf)
        {
            try
            {

                if (usuario.Password != passwordConf)
                    ModelState.AddModelError("PasswordConf", "Las contraseñas no coinciden");

                var usuarios = context.Usuarios.ToList();
                foreach (var item in usuarios)
                {
                    if (item.Correo == usuario.Correo)
                        ModelState.AddModelError("Correo", "Este correo ya se encuentra registrado");
                    if (item.Username == usuario.Username)
                        ModelState.AddModelError("Username", "Este usuario ya existe, intenta otro");
                }

                if (ModelState.IsValid)
                {
                    if (perfil != null)
                        usuario.Imagen = SaveFile(perfil);

                    usuario.Password = CreateHash(usuario.Password);
                    usuario.IdRol = 3;
                    context.Usuarios.Add(usuario);
                    context.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                    return View(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Recover()
        {
            ViewBag.Codigo = false;
            return View(new Recovery());
        }

        [HttpPost]
        public IActionResult Recover(Recovery recovery)
        {
            try
            {
                ViewBag.Codigo = false;
                var users = context.Usuarios.Where(o => o.Correo == recovery.Email).FirstOrDefault();
                if (users == null)
                {
                    ModelState.AddModelError("Email", "El usuario del correo no esta resgistrado");
                }
                if (ModelState.IsValid)
                {
                    Random random = new Random();
                    int num = random.Next(1000, 9999);
                    var numero = CreateHash(num.ToString());

                    users.Recovery = numero;
                    context.Usuarios.Update(users);
                    context.SaveChanges();

                    EmailSend(recovery.Email,numero);
                    ViewBag.Codigo = true;
                    return View(recovery);
                }
                else
                    return View(recovery);
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult StartRecover(string token)
        {
            RecoverP recoverp = new RecoverP();
            recoverp.Token = token;

            var users = context.Usuarios.Where(o => o.Recovery == recoverp.Token).FirstOrDefault();
            if (users == null || recoverp.Token == null || recoverp.Token.Trim().Equals(""))
            {
                return RedirectToAction("Login");
            }
            return View(recoverp);
        }

        [HttpPost]
        public IActionResult StartRecover(RecoverP recoverp)
        {
            try
            {
                var users = context.Usuarios.Where(o => o.Recovery == recoverp.Token).FirstOrDefault();
                if (ModelState.IsValid)
                {
                    users.Password = CreateHash(recoverp.Password);
                    users.Recovery = null;
                    context.Usuarios.Update(users);
                    context.SaveChanges();
                }
                else
                    return View(recoverp);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return RedirectToAction("Login");
        }

        private string CreateHash(string input)
        {
            input += configuration.GetValue<string>("Token");
            var sha = SHA512.Create();
            var bytes = Encoding.Default.GetBytes(input);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
        
        protected Usuario GetLoggedUser()
        {
            var claim = HttpContext.User.Claims.FirstOrDefault();
            var user = context.Usuarios.Where(o => o.Username == claim.Value).FirstOrDefault();
            return user;
        }

        private string SaveFile(IFormFile file)
        {
            string relativePath = "";
            if (file.Length > 0 && (file.ContentType == "image/jpeg" || file.ContentType == "image/png" || file.ContentType == "image/gif"))
            {
                relativePath = Path.Combine("FilesBD", file.FileName);
                var filePath = Path.Combine(hosting.WebRootPath, relativePath);
                var stream = new FileStream(filePath, FileMode.Create);
                file.CopyTo(stream);
                stream.Close();
            }
            return "/" + relativePath.Replace('\\', '/');
        }

        protected void EmailSend(string correoUser,string token)
        {
            string url = urlDomain + "user/startrecover/?token=" + token;
            MailMessage mailMessage = new MailMessage(email, correoUser,
                "Recuperación de la cuenta en ORACON.SRL",
                "<p>Correo de recuperacion de contraseña</p><br>"
                + "<a href='" + url + "'><strong>Click para recuperar</strong></a>")
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
    }
}
