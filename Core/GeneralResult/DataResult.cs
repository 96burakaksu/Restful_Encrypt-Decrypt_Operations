using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.GeneralResult
{
    public class DataResult<T> : Result
    {
        public T Data { get; set; }

        public static DataResult<T> SuccessData(T data)
        {
            return new DataResult<T> { Data = data, StatusCode = 200, Success = true };
        }
        public static DataResult<T> Fail(int statusCode, string error)
        {
            return new DataResult<T> { StatusCode = statusCode, Message = error };
        }
    }
}
