using GestaoClinicaEstetica.Application.Models;
using GestaoClinicaEstetica.Domain.Entidades;
using GestaoClinicaEstetica.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GestaoClinicaEstetica.Application.Controllers
{
    public class SegurancaController : Controller
    {
        private IUsuarioService _usuarioService;

        public SegurancaController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // GET: Login
        public ActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl ?? "/GEPV/Home";
            return View();
        }

        [HttpPost]
        public ActionResult Entrar(LoginModel loginDto, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            Usuario usuario = new Usuario
            {
                Login = loginDto.Usuario,
                Senha = loginDto.Senha
            };

            var usuarioDados = _usuarioService.Logar(usuario);

            if (usuarioDados == null)
                return View("Index", loginDto);

            FormsAuthentication.SetAuthCookie(loginDto.Usuario, false);

            Session["displayName"] = usuarioDados.Nome;
            Session["loginUsuario"] = usuarioDados.Login;
            
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return View("Index", new LoginModel());
        }
    }
}