using Domain.Amenities;

namespace Application.Amenities.Exceptions;

public abstract class AmenityException(AmenityId amenityId, string message, Exception? innerException = null)
    : Exception(message, innerException)
{
    public AmenityId AmenityId { get; } = amenityId;
}

public class AmenityAlreadyExistsException(AmenityId amenityId) : AmenityException(amenityId, $"Amenity already exists under id {amenityId}");
public class AmenityNotFoundException(AmenityId amenityId) : AmenityException(amenityId, $"Amenity not found under id {amenityId}");
public class AmenityUnhandledException(AmenityId amenityId, Exception? innerException = null) : AmenityException(amenityId, $"Unhandled exception under id {amenityId}", innerException);