using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Example.Client.Modles.Dtos
{
    public class ApiResponse<T>
    {
        public ApiResponse()
        {
            StatusCode = HttpStatusCode.OK;
            Message = "OK";
        }

        public ApiResponse(T result)
        {
            StatusCode = HttpStatusCode.OK;
            Message = "OK";
            Result = result;
        }

        public ApiResponse(HttpStatusCode statusCode, string message, string exception = "")
        {
            StatusCode = statusCode;
            Message = message;
            Exception = exception;
        }

        public HttpStatusCode StatusCode { get; }
        public string Message { get; }
        public string Exception { get; }
        public T Result { get; }
    }
}