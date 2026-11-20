using Domain.Amenities;
using LanguageExt;

namespace Application.Common.Interfaces.Queries;

public interface IAmenityQueries
{
    Task<IReadOnlyList<Amenity>> GetAll(CancellationToken cancellationToken);
    Task<Option<Amenity>> GetById(AmenityId id, CancellationToken cancellationToken);
}