using ChatSystem.DTO.DtoResponce;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.BLL.Interfaces
{
    public interface IReadingService
    {
        Task<List<ReadingResponse>> GetReadingsAsync(int patientId);
    }
}
