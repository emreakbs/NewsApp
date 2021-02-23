using ImageApp.DataAccess.Repository.EFReporistory;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageApp.DataAccess.UnitOfWork
{
    /// <summary>
    /// UnitOfWork sınıfı tarafından kullanılacak arayüz.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IEFRepository<T> GetRepository<T>() where T : class;
        int SaveChanges();
    }
}
