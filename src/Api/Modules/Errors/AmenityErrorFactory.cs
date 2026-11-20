using Application.Amenities.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Errors;

public static class AmenityErrorFactory
{
    public static ObjectResult ToObjectResult(this AmenityException error)
    {
        return new ObjectResult(error.Message)
        {
            StatusCode = error switch
            {
                AmenityAlreadyExistsException => StatusCodes.Status409Conflict,
                AmenityNotFoundException => StatusCodes.Status404NotFound,
                AmenityUnhandledException => StatusCodes.Status500InternalServerError,
                _ => throw new NotImplementedException("Amenity error handler is not implemented.")
            }
        };
    }
}