using System;
using System.Collections.Generic;
using System.Text;
using ChatSystem.DAL.Models;
using ChatSystem.DTO.DtoRequests;
using ChatSystem.DTO.DtoResponce;

namespace ChatSystem.BLL.Mapping
{
    public class ChatSystemProfile : AutoMapper.Profile
    {
        public ChatSystemProfile() {
            CreateMap<Chat, ApiResponce<ChatResponce>>() ;
            CreateMap<Chat, ChatResponce>();

            CreateMap<ChatRequest, Chat>();
            CreateMap<DoctorPatientRequest, Chat>();
            CreateMap<Chat,ApiResponce<DoctorPatientResponce>>() ;
            CreateMap<Message,MessageResponce>() ;

        }
    }
}
