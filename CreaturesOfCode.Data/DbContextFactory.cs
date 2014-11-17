using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreaturesOfCode.Data
{
    public interface IDbContextFactory
    {
        DbContext GetContext();
    }
    public class DbContextFactory : IDbContextFactory
    {
        private readonly DbContext _context;
        public DbContextFactory()
        {
            _context = new CoCDbContext();
        }
        public DbContext GetContext()
        {
            return _context;
        }
    }
}
