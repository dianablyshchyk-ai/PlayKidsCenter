using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.Amenities;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class AmenityRepository(ApplicationDbContext context) : IAmenityRepository, IAmenityQueries
{
    public async Task<IReadOnlyList<Amenity>> GetAll(CancellationToken cancellationToken)
    {
        return await context.Amenities
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Option<Amenity>> GetById(AmenityId id, CancellationToken cancellationToken)
    {
        var entity = await context.Amenities
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity ?? Option<Amenity>.None;
    }

    public async Task<Option<Amenity>> GetByName(string name, CancellationToken cancellationToken)
    {
        var entity = await context.Amenities
            .FirstOrDefaultAsync(x => x.Name == name, cancellationToken);

        return entity ?? Option<Amenity>.None;
    }

    public async Task<IReadOnlyList<Amenity>> GetByIds(IReadOnlyList<AmenityId> ids, CancellationToken cancellationToken)
    {
        return await context.Amenities
            .Where(x => ids.Contains(x.Id))
            .ToListAsync(cancellationToken);
    }

    public async Task<Amenity> Add(Amenity amenity, CancellationToken cancellationToken)
    {
        await context.Amenities.AddAsync(amenity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return amenity;
    }
}