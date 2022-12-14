using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.DTOs
{
    public class CustomResponseDTO
    {
        public string Data { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
