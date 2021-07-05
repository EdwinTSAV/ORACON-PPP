using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oracon.Models;
using Oracon.Models.Class;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Oracon.Repositorio;
using Oracon.Servicios;

namespace Oracon.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepo context;
        private readonly IClaimService claim;

        public UserController(IUserRepo context, IClaimService claim)
        {
            this.context = context;
            this.claim = claim;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = context.GetUsuario(username, password);

            if (user != null)
            {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, username)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                claim.SetHttpContext(HttpContext);
                claim.Login(claimsPrincipal);

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
            claim.SetHttpContext(HttpContext);
            claim.Logout();
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

                var usuarios = context.GetUsers();
                foreach (var item in usuarios)
                {
                    if (item.Correo == usuario.Correo)
                        ModelState.AddModelError("Correo", "Este correo ya se encuentra registrado");
                    if (item.Username == usuario.Username)
                        ModelState.AddModelError("Username", "Este usuario ya existe, intenta otro");
                }

                if (ModelState.IsValid)
                {
                    context.SaveUsers(usuario, perfil);
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
                var users = context.GetUsuarioEmail(recovery);
                if (users == null)
                {
                    ModelState.AddModelError("Email", "El usuario del correo no esta resgistrado");
                }
                if (ModelState.IsValid)
                {
                    context.ChangePass(recovery,users);
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
            RecoverP recoverp = new RecoverP
            {
                Token = token
            };

            var users = context.GetUsuarioRecover(token);
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
                string token = recoverp.Token;
                var users = context.GetUsuarioRecover(token);
                if (ModelState.IsValid)
                {
                    context.SavePassword(users,recoverp);
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
    }
}
