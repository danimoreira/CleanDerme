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
    public class ClientesController : BaseController
    {
        private IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        // GET: Clientes
        public ActionResult Index()
        {
            UpdateBag();
            return View(_clienteService.List());
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = _clienteService.GetById(id.Value);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Cpf,DataNascimento,Nome,Endereco,Bairro,Cidade,Uf,Cep,TelefoneFixo,TelefoneCelular,Email,DataCadastro,UsuarioCadastro,DataAlteracao,UsuarioAlteracao")] Cliente cliente)
        {
            cliente.DataCadastro = DateTime.Now;
            cliente.UsuarioCadastro = ViewBag.UsuarioLogin;
            cliente.DataAlteracao = DateTime.Now;
            cliente.UsuarioAlteracao = ViewBag.UsuarioLogin;

            ModelState.Clear();
            TryValidateModel(cliente);

            if (ModelState.IsValid)
            {
                _clienteService.Add(cliente);                
                return RedirectToAction("Index");
            }

            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = _clienteService.GetById(id.Value);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Cpf,DataNascimento,Nome,Endereco,Bairro,Cidade,Uf,Cep,TelefoneFixo,TelefoneCelular,Email,DataCadastro,UsuarioCadastro,DataAlteracao,UsuarioAlteracao")] Cliente cliente)
        {
            cliente.DataAlteracao = DateTime.Now;
            cliente.UsuarioAlteracao = ViewBag.UsuarioLogin;

            ModelState.Clear();
            TryValidateModel(cliente);

            if (ModelState.IsValid)
            {
                _clienteService.Update(cliente);
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = _clienteService.GetById(id.Value);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = _clienteService.GetById(id);
            _clienteService.Delete(cliente);
            
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _clienteService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
