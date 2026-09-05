using ChatSystem.DTO.DtoRequests;
using ChatSystem.DTO.DtoResponce;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.BLL.Interfaces
{
    public interface IReadingService
    {
        Task<List<ReadingResponse>> GetReadingsAsync(int patientId);
        Task<ApiResponce<bool>> AddReadingAsync(ReadingRequest request, string userId);
        Task<ApiResponce<bool>> UpdateReadingAsync(UpdateReadingRequest request);
        Task<ApiResponce<bool>> DeleteReadingAsync(int readingId);
    }
}
