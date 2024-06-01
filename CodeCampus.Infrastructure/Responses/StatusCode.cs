namespace CodeCampus.Infrastructure.Responses;

public enum StatusCode
{
    OK = 200,
    ERROR = 400,
    ACCESS_DENIED = 403,
    NOT_FOUND = 404,
    EXISTS = 409,
    UNAUTHORIZED = 401,
    INTERNAL_SERVER_ERROR = 500
}

