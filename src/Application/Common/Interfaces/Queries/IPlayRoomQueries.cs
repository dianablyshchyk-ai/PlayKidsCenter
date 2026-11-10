using Domain.PlayRooms;
using LanguageExt;

namespace Application.Common.Interfaces.Queries;

public interface IPlayRoomQueries
{
    Task<IReadOnlyList<PlayRoom>> GetAll(CancellationToken cancellationToken);
    Task<Option<PlayRoom>> GetById(PlayRoomId id, CancellationToken cancellationToken);
}