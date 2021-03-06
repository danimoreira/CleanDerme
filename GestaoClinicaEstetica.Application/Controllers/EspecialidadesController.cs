﻿using System;
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
    public class EspecialidadesController : BaseController
    {
        private IEspecialidadeService _especialidadeService;

        public EspecialidadesController(IEspecialidadeService especialidadeService)
        {
            _especialidadeService = especialidadeService;
        }

        // GET: Especialidades
        public ActionResult Index()
        {
            UpdateBag();
            return View(_especialidadeService.List());
        }

        // GET: Especialidades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Especialidade especialidade = _especialidadeService.GetById(id.Value);
            if (especialidade == null)
            {
                return HttpNotFound();
            }
            return View(especialidade);
        }

        // GET: Especialidades/Create
        public ActionResult Create()
        {
            UpdateBag();
            return View();
        }

        // POST: Especialidades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Especialidade especialidade)
        {
            UpdateBag();

            especialidade.DataCadastro = DateTime.Now;
            especialidade.UsuarioCadastro = ViewBag.UsuarioLogin;
            especialidade.DataAlteracao = DateTime.Now;
            especialidade.UsuarioAlteracao = ViewBag.UsuarioLogin;

            ModelState.Clear();
            TryValidateModel(especialidade);

            if (ModelState.IsValid)
            {
                _especialidadeService.Add(especialidade);
                
                return RedirectToAction("Index");
            }

            return View(especialidade);
        }

        // GET: Especialidades/Edit/5
        public ActionResult Edit(int? id)
        {
            UpdateBag();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Especialidade especialidade = _especialidadeService.GetById(id.Value);

            if (especialidade == null)
            {
                return HttpNotFound();
            }
            return View(especialidade);
        }

        // POST: Especialidades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Especialidade especialidade)
        {
            UpdateBag();

            especialidade.DataAlteracao = DateTime.Now;
            especialidade.UsuarioAlteracao = ViewBag.UsuarioLogin;

            ModelState.Clear();
            TryValidateModel(especialidade);

            if (ModelState.IsValid)
            {
                _especialidadeService.Update(especialidade);
                return RedirectToAction("Index");
            }
            return View(especialidade);
        }

        // GET: Especialidades/Delete/5
        public ActionResult Delete(int? id)
        {
            UpdateBag();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Especialidade especialidade = _especialidadeService.GetById(id.Value);

            if (especialidade == null)
            {
                return HttpNotFound();
            }
            return View(especialidade);
        }

        // POST: Especialidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UpdateBag();

            Especialidade especialidade = _especialidadeService.GetById(id);
            _especialidadeService.Delete(especialidade);
            
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _especialidadeService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
