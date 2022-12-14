using AuthService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.DTOs
{
    public class CustomResponse
    {
        public static CustomResponseDTO Success(string data)
        {
            return new CustomResponseDTO { Data = data, StatusCode = 200 ,Success=true};
        }

        public static CustomResponseDTO Fail(int statusCode, string error)
        {
            return new CustomResponseDTO { StatusCode = statusCode, Message = error };
        }
    }
}
