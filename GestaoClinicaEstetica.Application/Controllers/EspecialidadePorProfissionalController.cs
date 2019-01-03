using GestaoClinicaEstetica.Application.Controllers.Base;
using GestaoClinicaEstetica.Application.Models;
using GestaoClinicaEstetica.Domain.Entidades;
using GestaoClinicaEstetica.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestaoClinicaEstetica.Application.Controllers
{
    public class EspecialidadePorProfissionalController : BaseController
    {
        private IProfissionalService _profissionalService;
        private IEspecialidadeService _especialidadeService;
        private IEspecialidadePorProfissionalService _especialidadePorProfissionalService;

        public EspecialidadePorProfissionalController(IProfissionalService profissionalService, IEspecialidadeService especialidadeService, IEspecialidadePorProfissionalService especialidadePorProfissionalService)
        {
            _profissionalService = profissionalService;
            _especialidadeService = especialidadeService;
            _especialidadePorProfissionalService = especialidadePorProfissionalService;
        }

        // GET: EspecialidadePorProfissional
        public ActionResult Index()
        {
            UpdateBag();

            return View();
        }

        public JsonResult RecuperarEspecialidadesDoProfissional(int codProfissional)
        {
            List<ListaEspecialidadeViewModel> especialidades = new List<ListaEspecialidadeViewModel>();
            var listaEspecialidades = _especialidadeService.List();
            var especialidadesDoProfissional = _profissionalService.GetById(codProfissional).EspecialidadePorProfissional.ToList();

            foreach (var item in listaEspecialidades)
            {
                var especialidadeSelecionado = especialidadesDoProfissional.Where(x => x.CodEspecialidade.Equals(item.Id)).FirstOrDefault();

                especialidades.Add(new ListaEspecialidadeViewModel()
                {
                    IdObjeto = especialidadeSelecionado != null ? especialidadeSelecionado.Id : 0,
                    CodEspecialidade = item.Id,
                    CodProfissional = codProfissional,
                    DescricaoEspecialidade = item.Descricao,
                    Selecionado = especialidadesDoProfissional.Where(x => x.CodEspecialidade.Equals(item.Id)).Count() > 0
                });
            }

            return Json(especialidades, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [Route("EspecialidadePorProfissional/SalvarEspecialidades")]
        public void SalvarEspecialidades(IEnumerable<ListaEspecialidadeViewModel> objetos)
        {
            foreach (var item in objetos)
            {
                var objEspecialidade = _especialidadePorProfissionalService.List().Where(x => x.Id == item.IdObjeto).FirstOrDefault();
                if (objEspecialidade == null)
                {
                    if (item.Selecionado)
                    {
                        EspecialidadePorProfissional especialidadeNovo = new EspecialidadePorProfissional()
                        {
                            CodEspecialidade = item.CodEspecialidade,
                            CodProfissional = item.CodProfissional,
                            DataCadastro = DateTime.Now,
                            UsuarioCadastro = ViewBag.UsuarioLogin
                        };

                        _especialidadePorProfissionalService.Add(especialidadeNovo);
                    }
                }
                else
                {
                    if (!item.Selecionado)
                    {
                        _especialidadePorProfissionalService.Delete(objEspecialidade);
                    }
                }
            }
        }

        public override void UpdateBag()
        {
            base.UpdateBag();

            ViewBag.ListaProfissional = _profissionalService.List().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Nome
            }).OrderBy(y => y.Text).ToList();
        }
    }
}