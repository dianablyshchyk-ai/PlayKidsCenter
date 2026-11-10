using Application.PlayRooms.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Errors;

public static class PlayRoomErrorFactory
{
    public static ObjectResult ToObjectResult(this PlayRoomException error)
    {
        return new ObjectResult(error.Message)
        {
            StatusCode = error switch
            {
                PlayRoomAlreadyExistsException => StatusCodes.Status409Conflict,
                PlayRoomNotFoundException => StatusCodes.Status404NotFound,
                PlayRoomUnhandledException => StatusCodes.Status500InternalServerError,
                _ => throw new NotImplementedException("PlayRoom error handler is not implemented.")
            }
        };
    }
}