using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreEntities.Enities;
using Repository.InterFace;

namespace Repository.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Contact> Contacts { get; }

        Task<int> CompleteAsync(); // Commit all changes
    }
}
