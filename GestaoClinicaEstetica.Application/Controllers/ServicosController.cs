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
using GestaoClinicaEstetica.Ioc;
using Ninject;

namespace GestaoClinicaEstetica.Application.Controllers
{
    public class ServicosController : BaseController
    {
        private IServicoService _servicoService;
        private IEspecialidadeService _especialidadeService;

        public ServicosController(IServicoService servicoService, IEspecialidadeService especialidadeService)
        {
            _servicoService = servicoService;
            _especialidadeService = especialidadeService;
        }
        
        // GET: Servico5s
        public ActionResult Index()
        {
            return View(_servicoService.List());
        }

        // GET: Servicos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servico servico = _servicoService.GetById(id.Value);
            if (servico == null)
            {
                return HttpNotFound();
            }
            return View(servico);
        }

        // GET: Servicos/Create
        public ActionResult Create()
        {
            UpdateBag();
            return View();
        }

        // POST: Servicos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descricao,ValorServico,Periodicidade,CodigoEspecialidade,QuantidadeSessoes,DataCadastro,UsuarioCadastro,DataAlteracao,UsuarioAlteracao")] Servico servico)
        {
            UpdateBag();

            servico.DataCadastro = DateTime.Now;
            servico.UsuarioCadastro = ViewBag.UsuarioLogin;
            servico.DataAlteracao = DateTime.Now;
            servico.UsuarioAlteracao = ViewBag.UsuarioLogin;

            ModelState.Clear();
            TryValidateModel(servico);

            if (ModelState.IsValid)
            {
                _servicoService.Add(servico);
                
                return RedirectToAction("Index");
            }

            return View(servico);
        }

        // GET: Servicos/Edit/5
        public ActionResult Edit(int? id)
        {
            UpdateBag();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servico servico = _servicoService.GetById(id.Value);
            if (servico == null)
            {
                return HttpNotFound();
            }
            return View(servico);
        }

        // POST: Servicos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descricao,ValorServico,Periodicidade,CodigoEspecialidade,QuantidadeSessoes,DataCadastro,UsuarioCadastro,DataAlteracao,UsuarioAlteracao")] Servico servico)
        {
            servico.DataAlteracao = DateTime.Now;
            servico.UsuarioAlteracao = ViewBag.UsuarioLogin;

            ModelState.Clear();
            TryValidateModel(servico);

            if (ModelState.IsValid)
            {
                _servicoService.Update(servico);
                return RedirectToAction("Index");
            }
            return View(servico);
        }

        // GET: Servicos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servico servico = _servicoService.GetById(id.Value);
            if (servico == null)
            {
                return HttpNotFound();
            }
            return View(servico);
        }

        // POST: Servicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Servico servico = _servicoService.GetById(id);
            _servicoService.Delete(servico);
            
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _servicoService.Dispose();
            }
            base.Dispose(disposing);
        }

        public override void UpdateBag()
        {
            base.UpdateBag();

            ViewBag.ListaEspecialidades = _especialidadeService.List().Select(x => new SelectListItem()
            {
                Text = x.Descricao,
                Value = x.Id.ToString()
            }).ToList();
        }
    }
}
