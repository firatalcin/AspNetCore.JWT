using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SharedLibrary.Dtos
{
    public class Response<T> where T : class
    {
        public T Data { get; set; }
        public int StatusCode { get; set; }
        [JsonIgnore]
        public bool IsSuccessful { get; private set; }
        public ErrorDto Error { get; private set; }

        public static Response<T> Success(T data, int statusCode, bool isSuccessful)
        {
            return new Response<T> { StatusCode = statusCode, Data = data , IsSuccessful = true};
        }

        public static Response<T> Success(int statusCode, bool isSuccessful)
        {
            return new Response<T> { StatusCode = statusCode, Data = default, IsSuccessful = true};
        }

        public static Response<T> Fail(ErrorDto errorDto, int statusCode, bool isSuccessful)
        {
            return new Response<T> { Error = errorDto, StatusCode = statusCode , IsSuccessful = false};
        }

        public static Response<T> Fail(string errorMessage, int statusCode, bool isShow, bool isSuccessful)
        {
            var errorDto = new ErrorDto(errorMessage, isShow);

            return new Response<T> { Error = errorDto, StatusCode = statusCode , IsSuccessful = false};
        }
    }
}
