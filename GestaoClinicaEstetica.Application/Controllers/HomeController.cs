using GestaoClinicaEstetica.Application.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestaoClinicaEstetica.Application.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            this.UpdateBag();
            if (string.IsNullOrEmpty(ViewBag.UsuarioLogin))
                return RedirectToAction("Index", "Seguranca");
            
            return View();
        }
    }
}