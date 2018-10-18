using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GestaoClinicaEstetica.Application.Controllers.Base;
using GestaoClinicaEstetica.Context;
using GestaoClinicaEstetica.Domain.Entidades;
using GestaoClinicaEstetica.Domain.Interfaces.Service;

namespace GestaoClinicaEstetica.Application.Controllers
{
    public class UsuariosController : BaseController
    {
        private IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // GET: Usuarios
        public ActionResult Index()
        {
            return View(_usuarioService.List());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = _usuarioService.GetById(id.Value);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Login,Senha,ConfirmaSenha,DataCadastro,UsuarioCadastro,DataAlteracao,UsuarioAlteracao")] Usuario usuario)
        {
            UpdateBag();

            usuario.DataCadastro = DateTime.Now;
            usuario.UsuarioCadastro = ViewBag.UsuarioLogin;
            usuario.DataAlteracao = DateTime.Now;
            usuario.UsuarioAlteracao = ViewBag.UsuarioLogin;

            ModelState.Clear();
            TryValidateModel(usuario);

            if (ModelState.IsValid)
            {
                _usuarioService.Add(usuario);                
                return RedirectToAction("Index");
            }

            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = _usuarioService.GetById(id.Value);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Login,Senha,ConfirmaSenha,DataCadastro,UsuarioCadastro,DataAlteracao,UsuarioAlteracao")] Usuario usuario)
        {
            usuario.DataAlteracao = DateTime.Now;
            usuario.UsuarioAlteracao = ViewBag.UsuarioLogin;

            ModelState.Clear();
            TryValidateModel(usuario);

            if (ModelState.IsValid)
            {
                _usuarioService.Update(usuario);
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = _usuarioService.GetById(id.Value);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = _usuarioService.GetById(id);
            _usuarioService.Delete(usuario);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _usuarioService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
