using Domain.Amenities;
using LanguageExt;

namespace Application.Common.Interfaces.Repositories;

public interface IAmenityRepository
{
    Task<Option<Amenity>> GetById(AmenityId id, CancellationToken cancellationToken);
    Task<Option<Amenity>> GetByName(string name, CancellationToken cancellationToken);
    Task<IReadOnlyList<Amenity>> GetByIds(IReadOnlyList<AmenityId> ids, CancellationToken cancellationToken);
    Task<Amenity> Add(Amenity amenity, CancellationToken cancellationToken);
}