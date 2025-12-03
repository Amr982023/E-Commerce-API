using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Core.Interfaces.Unit_Of_Work_Interface;

namespace E_commerce_Infrastructure.Repositories.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        //interfaces

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            //interfaces
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
