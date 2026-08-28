using ChatSystem.DAL.Data;
using ChatSystem.DAL.Models;
using ChatSystem.DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ChatSystem.DAL.Repository.Classes
{
    public class MessageRepository:Repository<Message>, IMessageRepository
    {
        private readonly ApplicationDbContext _context;
        public MessageRepository(ApplicationDbContext context):base(context) {
        _context = context;
        }

        public async Task<List<Message>> getByChatId(int chatId)
        {
            return await _context.Messages.Where(c=>c.ChatId == chatId).ToListAsync();
        }

        public async Task<bool?> isReading(int messageId)
        {
            var status= await _context.Messages.FindAsync(messageId);
            if (status == null)
            {
                return null;
            }
            return status.IsRead;
        }

        public async Task<List<Message>?> MarkMessagesAsReadAsync(int chatId, string userId)
        {
            var result=await _context.Messages.Where(m=>m.ChatId== chatId && m.SenderId != userId && !m.IsRead).ToListAsync();
            if (result.Any()) {
                foreach (var item in result)
                {
                    item.IsRead = true;
                }
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
