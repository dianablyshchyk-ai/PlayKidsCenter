using Domain.PlayRooms;
using LanguageExt;

namespace Application.Common.Interfaces.Repositories;

public interface IPlayRoomRepository
{
    Task<Option<PlayRoom>> GetById(PlayRoomId id, CancellationToken cancellationToken);
    Task<Option<PlayRoom>> GetByRoomNumber(string roomNumber, CancellationToken cancellationToken);
    Task<Option<PlayRoom>> GetByName(string name, CancellationToken cancellationToken);
    Task<PlayRoom> Add(PlayRoom playRoom, CancellationToken cancellationToken);
    Task<PlayRoom> Update(PlayRoom playRoom, CancellationToken cancellationToken);
    Task Delete(PlayRoom playRoom, CancellationToken cancellationToken);
}