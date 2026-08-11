using ChatSystem.DAL.Data;
using ChatSystem.DAL.Models;
using ChatSystem.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DAL.Repository.Classes
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _Context;
        public Repository(ApplicationDbContext context)
        {
            _Context = context;
        }
        public async Task<T> Create(T entity)
        {
           await _Context.Set<T>().AddAsync(entity);
           await _Context.SaveChangesAsync();
           return entity;
        }

        public async Task<bool> DeleteById(int id)
        {
            var entity = await _Context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            _Context.Set<T>().Remove(entity);
            await _Context.SaveChangesAsync();
            return true;
        }

        public async Task<List<T>> GetAll()
        {
            return await _Context.Set<T>().ToListAsync();
        }
        

        public async Task<T> GetById(int id)
        {
            return await _Context.Set<T>().FindAsync(id);
        }

        public async Task<T> Update(T entity)
        {
            _Context.Set<T>().Update(entity);
            await _Context.SaveChangesAsync();
            return entity;
        
        }
    }
}
