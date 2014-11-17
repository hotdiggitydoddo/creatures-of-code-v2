using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CreaturesOfCode.Core;

namespace CreaturesOfCode.Data
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        TEntity Create(TEntity entity);
        TEntity Delete(TEntity entity);
        void Update(TEntity entity);
    }



    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly DbContext _context;

        public Repository(IUnitOfWork uow)
        {
            _context = uow.Context;
        }

        public TEntity Get(int id)
        {
            return _context.Set<TEntity>().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public TEntity Create(TEntity entity)
        {
            return _context.Set<TEntity>().Add(entity);
        }

        public TEntity Delete(TEntity entity)
        {
            return _context.Set<TEntity>().Remove(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
