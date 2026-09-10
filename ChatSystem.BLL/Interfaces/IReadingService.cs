using ChatSystem.DTO.DtoRequests;
using ChatSystem.DTO.DtoResponce;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.BLL.Interfaces
{
    public interface IReadingService
    {
        Task<ApiResponce<List<ReadingResponse>>> GetReadingsAsync(int patientId);
        Task<ApiResponce<bool>> AddReadingAsync(ReadingRequest request, string userId);
        Task<ApiResponce<bool>> UpdateReadingAsync(UpdateReadingRequest request);
        Task<ApiResponce<bool>> DeleteReadingAsync(int readingId);
        Task<ApiResponce<List<ReadingResponse>>> GetReadingForDoctorAsync(int patientId, string doctorUserId);
    }
}
