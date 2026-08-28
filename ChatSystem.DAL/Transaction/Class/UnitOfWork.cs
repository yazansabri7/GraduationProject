using ChatSystem.DAL.Data;
using ChatSystem.DAL.Transaction.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DAL.Transaction.Class
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }   
        public async Task BeginTransactionAsync()
        {
             await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public async Task RollbackAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }

        public async Task SaveChangesAsync()
        {
             await _context.SaveChangesAsync();
        }
    }
}

