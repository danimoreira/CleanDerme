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
    public class ProfissionaisController : BaseController
    {
        private IProfissionalService _profissionalService;

        public ProfissionaisController(IProfissionalService profissionalService)
        {
            _profissionalService = profissionalService;
        }

        // GET: Profissionais
        public ActionResult Index()
        {
            UpdateBag();
            return View(_profissionalService.List());
        }

        // GET: Profissionais/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profissional profissional = _profissionalService.GetById(id.Value);
            if (profissional == null)
            {
                return HttpNotFound();
            }
            return View(profissional);
        }

        // GET: Profissionais/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Profissionais/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Cpf,DataNascimento,Nome,Endereco,Bairro,Cidade,Uf,Cep,TelefoneFixo,TelefoneCelular,Email,DataCadastro,UsuarioCadastro,DataAlteracao,UsuarioAlteracao")] Profissional profissional)
        {
            UpdateBag();

            profissional.DataCadastro = DateTime.Now;
            profissional.UsuarioCadastro = ViewBag.UsuarioLogin;
            profissional.DataAlteracao = DateTime.Now;
            profissional.UsuarioAlteracao = ViewBag.UsuarioLogin;

            ModelState.Clear();
            TryValidateModel(profissional);

            if (ModelState.IsValid)
            {
                _profissionalService.Add(profissional);
                
                return RedirectToAction("Index");
            }

            return View(profissional);
        }

        // GET: Profissionais/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profissional profissional = _profissionalService.GetById(id.Value);
            if (profissional == null)
            {
                return HttpNotFound();
            }
            return View(profissional);
        }

        // POST: Profissionais/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Cpf,DataNascimento,Nome,Endereco,Bairro,Cidade,Uf,Cep,TelefoneFixo,TelefoneCelular,Email,DataCadastro,UsuarioCadastro,DataAlteracao,UsuarioAlteracao")] Profissional profissional)
        {
            profissional.DataAlteracao = DateTime.Now;
            profissional.UsuarioAlteracao = ViewBag.UsuarioLogin;

            ModelState.Clear();
            TryValidateModel(profissional);

            if (ModelState.IsValid)
            {
                _profissionalService.Update(profissional);
                return RedirectToAction("Index");
            }
            return View(profissional);
        }

        // GET: Profissionais/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profissional profissional = _profissionalService.GetById(id.Value);
            if (profissional == null)
            {
                return HttpNotFound();
            }
            return View(profissional);
        }

        // POST: Profissionais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Profissional profissional = _profissionalService.GetById(id);
            _profissionalService.Delete(profissional);
            
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _profissionalService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
