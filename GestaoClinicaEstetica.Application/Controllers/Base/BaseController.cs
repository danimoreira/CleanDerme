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
            if (Session["loginUsuario"] != null)
            {
                login = Session["loginUsuario"].ToString();
                usuarioName = Session["displayName"].ToString();
            }

            ViewBag.UsuarioLogin = login;
            ViewBag.UsuarioNome = usuarioName;
        }
    }
}