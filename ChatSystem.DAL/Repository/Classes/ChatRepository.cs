using ChatSystem.DAL.Data;
using ChatSystem.DAL.Models;
using ChatSystem.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DAL.Repository.Classes
{
    public class ChatRepository : IChatRepository
    {
        private readonly ApplicationDbContext _context;
        public ChatRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<Chat> AddAsync(Chat chat)
        {
            await _context.Chats.AddAsync(chat);
            return chat;
        }

        public async Task<List<Chat>> GetByDoctorIdAsync(int doctorId)
        {
            return await _context.Chats.Where(c=>c.DoctorId == doctorId).ToListAsync();
        }

        public async Task<Chat?> GetByDoctorPatientAsync(int doctorId, int patientId)
        {
            return await _context.Chats.AsNoTracking().FirstOrDefaultAsync(c => c.DoctorId == doctorId && c.PatientId == patientId);
        }

        public async Task<Chat?> GetByIdAsync(int chatId)
        {
           return await _context.Chats.Include(c => c.Messages).FirstOrDefaultAsync(c => c.Id == chatId);
        }

        public async Task<List<Chat>> GetByPatientIdAsync(int patientId)
        {
            var list=await _context.Chats.AsNoTracking().Where(c=>c.PatientId == patientId).ToListAsync();
            return list;
        }
    }
}
