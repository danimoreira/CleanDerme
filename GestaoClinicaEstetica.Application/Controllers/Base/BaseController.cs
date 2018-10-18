using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestaoClinicaEstetica.Application.Controllers.Base
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            ViewBag.UsuarioLogin = "daniel";
            ViewBag.UsuarioNome = "Daniel";
        }

        public virtual void UpdateBag()
        {

        }
    }
}