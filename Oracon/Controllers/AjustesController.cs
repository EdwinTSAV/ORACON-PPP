using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Oracon.Models;
using Oracon.Repositorio;
using Oracon.Servicios;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Oracon.Controllers
{
    [Authorize]
    public class AjustesController : Controller
    {
        private readonly IClaimService claim;
        private readonly IAjustesRepo context;
        private readonly IConfiguration configuration;

        public AjustesController(IAjustesRepo context, IClaimService claim, IConfiguration configuration)
        {
            this.context = context;
            this.claim = claim;
            this.configuration = configuration;
        }

        [HttpGet]
        public IActionResult Index()
        {
            claim.SetHttpContext(HttpContext);
            var user = context.GetUsuario(claim.GetLoggedUser().Id);
            ViewBag.User = user.IdRol;
            return View(user);
        }

        [HttpPost]
        public IActionResult Editar(Usuario usuario, IFormFile image)
        {
            claim.SetHttpContext(HttpContext);
            var user = context.GetUsuario(claim.GetLoggedUser().Id);
            ViewBag.User = user.IdRol;
            user.Nombres = usuario.Nombres;
            user.Apellidos = usuario.Apellidos;
            user.Celular = usuario.Celular;

            if (ModelState.IsValid)
            {
                context.UpdateUser(user, image);
            }
            return View("Index", user);
        }

        [HttpGet]
        public IActionResult RedesSociales()
        {
            claim.SetHttpContext(HttpContext);
            var user = context.GetUsuario(claim.GetLoggedUser().Id); 
            ViewBag.User = user.IdRol;
            return View(user);
        }

        [HttpPost]
        public IActionResult EditarRS(Usuario usuario)
        {
            claim.SetHttpContext(HttpContext);
            var user = context.GetUsuario(claim.GetLoggedUser().Id);
            ViewBag.User = user.IdRol;

            user.Twitter = usuario.Twitter;
            user.Facebook = usuario.Facebook;
            user.LinkedIn = usuario.LinkedIn;
            user.YouTube = usuario.YouTube;
            user.Instagram = usuario.Instagram;

            if (ModelState.IsValid)
            {
                context.UpdateUser(user, null);
            }
            return View("RedesSociales", user);
        }

        [HttpGet]
        public IActionResult Descripcion()
        {
            claim.SetHttpContext(HttpContext);
            var user = context.GetUsuario(claim.GetLoggedUser().Id);
            ViewBag.User = user.IdRol;
            return View(user);
        }

        [HttpPost]
        public IActionResult Descripcion(Usuario usuario)
        {
            claim.SetHttpContext(HttpContext);
            var user = context.GetUsuario(claim.GetLoggedUser().Id);
            ViewBag.User = user.IdRol;

            user.Titulo = usuario.Titulo;
            user.Biografia = usuario.Biografia;

            if (ModelState.IsValid)
            {
                context.UpdateUser(user, null);
            }

            return View(user);
        }

        [HttpGet]
        public IActionResult Security()
        {
            claim.SetHttpContext(HttpContext);
            var user = context.GetUsuario(claim.GetLoggedUser().Id);
            ViewBag.User = user.IdRol;
            return View(user);
        }

        [HttpPost]
        public IActionResult Security(Usuario usuario)
        {
            claim.SetHttpContext(HttpContext);
            var user = context.GetUsuario(claim.GetLoggedUser().Id);
            ViewBag.User = user.IdRol;
            var usuarios = context.GetUsuarios(claim.GetLoggedUser().Id);
            foreach (var item in usuarios)
            {
                if (item.Correo == usuario.Correo)
                    ModelState.AddModelError("Correo", "Este correo ya se encuentra registrado");
                if (item.Username == usuario.Username)
                    ModelState.AddModelError("Username", "Este usuario ya existe, intenta otro");
            }

            user.Correo = usuario.Correo;
            user.Username = usuario.Username;

            if (ModelState.IsValid)
            {
                context.UpdateUser(user, null);
            }

            return View(user);
        }

        [HttpPost]
        public IActionResult Password(Usuario usuario, string passwordActual, string passwordConf)
        {
            claim.SetHttpContext(HttpContext);
            var user = context.GetUsuario(claim.GetLoggedUser().Id);
            ViewBag.User = user.IdRol;
            
            if (CreateHash(passwordActual) != user.Password)
                ModelState.AddModelError("passwordActual", "Contraseña actual invalida");
            if (usuario.Password != passwordConf)
                ModelState.AddModelError("Password", "Las contraseñas no coinciden");

            else if (ModelState.IsValid)
            {
                user.Password = CreateHash(usuario.Password);
                context.UpdateUser(user, null);
            }

            return View("Security", user);
        }

        [HttpGet]
        public IActionResult Terminados()
        {
            claim.SetHttpContext(HttpContext);
            var user = context.GetUsuario(claim.GetLoggedUser().Id);
            ViewBag.User = user.IdRol;

            var terminados = context.GetCursoUsuariosT(user.Id);
            return View(terminados);
        }

        protected string CreateHash(string input)
        {
            input += configuration.GetValue<string>("Token");
            var sha = SHA512.Create();
            var bytes = Encoding.Default.GetBytes(input);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
