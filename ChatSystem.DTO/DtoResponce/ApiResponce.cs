using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DTO.DtoResponce
{
    public class ApiResponce<T> : BaseResponse
    {
        public T? Data { get; set; }
    }
}
