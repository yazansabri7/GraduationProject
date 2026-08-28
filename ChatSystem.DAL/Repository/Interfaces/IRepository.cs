using ChatSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DAL.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public Task<T?> GetById(int id);
        public Task<T> Create(T entity);
        public Task<T> Update(T entity);
        public Task<bool> DeleteById(int id);

        public Task<List<T>> GetAll();
    }
}
