using Domain.Amenities;

namespace Api.Dtos;

public record AmenityDto(
    Guid Id,
    string Name,
    DateTime CreatedAt)
{
    public static AmenityDto FromDomainModel(Amenity amenity)
        => new(
            Id: amenity.Id.Value,
            Name: amenity.Name,
            CreatedAt: amenity.CreatedAt);
}

public record CreateAmenityDto(string Name);