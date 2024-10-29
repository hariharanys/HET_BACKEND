using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using System.Net;

namespace HET_BACKEND.Utilities.ExceptionUtility
{
    public class ErrorMessage
    {
        public int GetStatusCode(Exception e)
        {
            return e switch
            {
                ArgumentNullException => (int)HttpStatusCode.BadRequest,
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                DbUpdateException=>(int)HttpStatusCode.Conflict,
                _ =>(int)HttpStatusCode.InternalServerError,
            };
        }

        public string GetErrorMessage(int StatusCode) {
            return StatusCode switch
            {
                400 => "Bad Request",
                401 => "UnAuthorized",
                404 => "Not Found",
                409 => "Db Conflict. Due to Duplicate Keys",
                419 => "Session Expired",
                500 => "Internal Server Error",
                _=>"An Unknown Error Occured"
            };
        }
    }
}
