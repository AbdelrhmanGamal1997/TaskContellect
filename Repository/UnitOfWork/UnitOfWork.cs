using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreEntities;
using CoreEntities.Enities;
using Repository.InterFace;
using Repository.RepositryPattern;

namespace Repository.UnitOfWork
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbcontext _context;

        public IRepository<Contact> Contacts { get; private set; }


        public UnitOfWork(AppDbcontext context)
        {
            _context = context;
            Contacts = new Repository<Contact>(_context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}