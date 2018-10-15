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
    public class ClinicasController : BaseController
    {
        private IClinicaService _clinicaService;

        public ClinicasController(IClinicaService clinicaService)
        {
            _clinicaService = clinicaService;
        }

        // GET: Clinicas
        public ActionResult Index()
        {
            return View(_clinicaService.List());
        }

        // GET: Clinicas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clinica clinica = _clinicaService.GetById(id.Value);
            if (clinica == null)
            {
                return HttpNotFound();
            }
            return View(clinica);
        }

        // GET: Clinicas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clinicas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Cnpj,DataFundacao,Nome,Endereco,Bairro,Cidade,Uf,Cep,TelefoneFixo,TelefoneCelular,Email,DataCadastro,UsuarioCadastro,DataAlteracao,UsuarioAlteracao")] Clinica clinica)
        {
            UpdateBag();

            clinica.DataCadastro = DateTime.Now;
            clinica.UsuarioCadastro = ViewBag.UsuarioLogin;
            clinica.DataAlteracao = DateTime.Now;
            clinica.UsuarioAlteracao = ViewBag.UsuarioLogin;

            ModelState.Clear();
            TryValidateModel(clinica);

            if (ModelState.IsValid)
            {
                _clinicaService.Add(clinica);                
                return RedirectToAction("Index");
            }

            return View(clinica);
        }

        // GET: Clinicas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clinica clinica = _clinicaService.GetById(id.Value);
            if (clinica == null)
            {
                return HttpNotFound();
            }
            return View(clinica);
        }

        // POST: Clinicas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Cnpj,DataFundacao,Nome,Endereco,Bairro,Cidade,Uf,Cep,TelefoneFixo,TelefoneCelular,Email,DataCadastro,UsuarioCadastro,DataAlteracao,UsuarioAlteracao")] Clinica clinica)
        {
            if (ModelState.IsValid)
            {
                _clinicaService.Update(clinica);
                return RedirectToAction("Index");
            }
            return View(clinica);
        }

        // GET: Clinicas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clinica clinica = _clinicaService.GetById(id.Value);
            if (clinica == null)
            {
                return HttpNotFound();
            }
            return View(clinica);
        }

        // POST: Clinicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Clinica clinica = _clinicaService.GetById(id);
            _clinicaService.Delete(clinica);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _clinicaService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
