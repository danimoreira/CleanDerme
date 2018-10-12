using GestaoClinicaEstetica.Domain.Interfaces.Repository.Base;
using GestaoClinicaEstetica.Domain.Interfaces.Service.Base;
using GestaoClinicaEstetica.Repository.Repository.Base;
using System.Collections.Generic;
using System.Linq;

namespace GestaoClinicaEstetica.Services.Services.Base
{
    public class ServiceBase<TEntity> : RepositoryBase<TEntity>, IServiceBase<TEntity> where TEntity : class
    {
        private readonly IRepositoryBase<TEntity> _repository;

        public ServiceBase(IRepositoryBase<TEntity> repository)
        {
            _repository = repository;
        }

        public virtual void Add(TEntity obj)
        {
            _repository.Add(obj);
        }

        public virtual void Delete(TEntity obj)
        {
            _repository.Delete(obj);
        }

        public override void Delete(int id)
        {
            _repository.Delete(id);
        }

        public override bool Exists(int id)
        {
            return _repository.Exists(id);
        }

        public override TEntity GetById(int id)
        {
            return _repository.GetById(id);
        }

        public override List<TEntity> List()
        {
            return _repository.List().ToList();
        }

        public virtual void Update(TEntity obj)
        {
            _repository.Update(obj);
        }

        public virtual void UpdateAll(List<TEntity> ListObj)
        {
            foreach (var item in ListObj)
            {
                this.Update(item);
            }
        }
    }
}
