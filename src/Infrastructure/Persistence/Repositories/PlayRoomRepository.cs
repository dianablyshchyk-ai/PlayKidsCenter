using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.PlayRooms;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class PlayRoomRepository(ApplicationDbContext context) : IPlayRoomRepository, IPlayRoomQueries
{
    public async Task<IReadOnlyList<PlayRoom>> GetAll(CancellationToken cancellationToken)
    {
        return await context.PlayRooms
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Option<PlayRoom>> GetById(PlayRoomId id, CancellationToken cancellationToken)
    {
        var entity = await context.PlayRooms
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity ?? Option<PlayRoom>.None;
    }

    public async Task<Option<PlayRoom>> GetByRoomNumber(string roomNumber, CancellationToken cancellationToken)
    {
        var entity = await context.PlayRooms
            .FirstOrDefaultAsync(x => x.RoomNumber == roomNumber, cancellationToken);

        return entity ?? Option<PlayRoom>.None;
    }

    public async Task<Option<PlayRoom>> GetByName(string name, CancellationToken cancellationToken)
    {
        var entity = await context.PlayRooms
            .FirstOrDefaultAsync(x => x.Name == name, cancellationToken);

        return entity ?? Option<PlayRoom>.None;
    }

    public async Task<PlayRoom> Add(PlayRoom playRoom, CancellationToken cancellationToken)
    {
        await context.PlayRooms.AddAsync(playRoom, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return playRoom;
    }

    public async Task<PlayRoom> Update(PlayRoom playRoom, CancellationToken cancellationToken)
    {
        context.PlayRooms.Update(playRoom);
        await context.SaveChangesAsync(cancellationToken);
        return playRoom;
    }

    public async Task Delete(PlayRoom playRoom, CancellationToken cancellationToken)
    {
        context.PlayRooms.Remove(playRoom);
        await context.SaveChangesAsync(cancellationToken);
    }
}