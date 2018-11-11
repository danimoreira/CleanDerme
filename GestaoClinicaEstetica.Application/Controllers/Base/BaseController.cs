using System.Web.Mvc;

namespace GestaoClinicaEstetica.Application.Controllers.Base
{
    public class BaseController : Controller
    {
        public BaseController()
        {

        }

        public virtual void UpdateBag()
        {
            var login = string.Empty;
            var usuarioName = string.Empty;

            if (HttpContext.Request.Cookies["displayName"] != null)
            {
                login = HttpContext.Request.Cookies["loginUsuario"].Value;
                usuarioName = HttpContext.Request.Cookies["displayName"].Value;
            }

            ViewBag.UsuarioLogin = login;
            ViewBag.UsuarioNome = usuarioName;
        }
    }
}