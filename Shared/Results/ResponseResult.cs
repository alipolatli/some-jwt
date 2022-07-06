using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class ResponseResult<TData>
    {
        public TData Data { get; private set; }

        public int StatusCode { get; private set; }

        public ErrorResult ErrorResult { get; private set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public bool IsSuccessfull { get; set; }

        public static ResponseResult<TData> Success(TData data, int statusCode)
        {
            return new ResponseResult<TData>() { Data = data, StatusCode = statusCode, IsSuccessfull = true };
        }

        public static ResponseResult<TData> Success(int statusCode)
        {
            return new ResponseResult<TData>() { Data = default, StatusCode = statusCode, IsSuccessfull = true };
        }

        public static ResponseResult<TData> Fail(ErrorResult errorResult, int statusCode)
        {
            return new ResponseResult<TData>() { ErrorResult = errorResult, StatusCode = statusCode, IsSuccessfull = false };
        }

        public static ResponseResult<TData> Fail(string error, int statusCode, bool isShow)
        {
            var errorResult = new ErrorResult(error, isShow);
            return new ResponseResult<TData>() { ErrorResult = errorResult, StatusCode = statusCode, IsSuccessfull = false };
        }

    }
}
