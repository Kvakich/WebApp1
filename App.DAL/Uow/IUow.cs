using App.DAL.Entities;
using App.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.Uow
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Phone> Phones { get; }
        IRepository<Order> Orders { get; }
        void Save();
    }
}
