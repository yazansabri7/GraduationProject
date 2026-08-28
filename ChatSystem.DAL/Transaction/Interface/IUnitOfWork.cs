using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DAL.Transaction.Interface
{
    public interface IUnitOfWork
    {
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
        Task SaveChangesAsync();
    }
}
