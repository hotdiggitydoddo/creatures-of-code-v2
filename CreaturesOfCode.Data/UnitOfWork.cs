using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace CreaturesOfCode.Data
{
    public interface IUnitOfWork
    {
        DbContext Context { get; }
        void SaveChanges();
    }

    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DbContext _context;
        private bool _disposed;


        public UnitOfWork(IDbContextFactory contextFactory)
        {
            _context = contextFactory.GetContext();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public DbContext Context { get { return _context; } }

        public void SaveChanges()
        {
            if (_context != null) 
                _context.SaveChanges();
        }
    }
}
