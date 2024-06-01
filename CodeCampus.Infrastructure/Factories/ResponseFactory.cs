using CodeCampus.Infrastructure.Responses;

namespace CodeCampus.Infrastructure.Factories;

public class ResponseFactory
{
    public static ResponseResult Ok()
    {
        return new ResponseResult
        {
            Message = "Succeeded",
            Status = StatusCode.OK
        };
    }

    public static ResponseResult Ok(object obj, string? message = null)
    {
        return new ResponseResult
        {
            ContentResult = obj,
            Message = message ?? "Succeeded",
            Status = StatusCode.OK
        };
    }

    public static ResponseResult Error(string? message = null)
    {
        return new ResponseResult
        {
            Message = message ?? "Failed",
            Status = StatusCode.ERROR
        };
    }

    public static ResponseResult AccessDenied(string? message = null)
    {
        return new ResponseResult
        {
            Message = message ?? "Denied",
            Status = StatusCode.ACCESS_DENIED
        };
    }

    public static ResponseResult NotFound(string? message = null)
    {
        return new ResponseResult
        {
            Message = message ?? "Not found",
            Status = StatusCode.NOT_FOUND
        };
    }

    public static ResponseResult Exists(string? message = null)
    {
        return new ResponseResult
        {
            Message = message ?? "Already exists",
            Status = StatusCode.EXISTS
        };
    }

    public static ResponseResult Unauthorized(string? message = null)
    {
        return new ResponseResult
        {
            Message = message ?? "Unauthorized",
            Status = StatusCode.UNAUTHORIZED
        };
    }
}
