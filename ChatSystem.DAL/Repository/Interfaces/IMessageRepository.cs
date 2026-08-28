using ChatSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DAL.Repository.Interfaces
{
    public interface IMessageRepository:IRepository<Message>
    {
        Task<List<Message>> getByChatId(int  chatId);

        Task<List<Message>?> MarkMessagesAsReadAsync(int chatId,string userId);
        Task<bool?>isReading(int messageId);
        
        
    }
}
