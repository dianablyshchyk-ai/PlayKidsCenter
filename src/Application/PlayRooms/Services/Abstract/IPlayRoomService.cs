using Domain.PlayRooms;

namespace Application.PlayRooms.Services.Abstract;

public interface IPlayRoomService
{
    Task<IReadOnlyList<PlayRoom>> GetPlayRooms(CancellationToken cancellationToken);
    Task<PlayRoom?> GetPlayRoom(Guid id, CancellationToken cancellationToken);
    Task<PlayRoom> Add(
        string roomNumber,
        string name,
        decimal pricePerHour,
        int capacity,
        bool hasAnimator,
        PlayRoomTheme theme,
        CancellationToken cancellationToken);
    Task<PlayRoom?> Update(
        Guid id,
        string roomNumber,
        string name,
        decimal pricePerHour,
        int capacity,
        bool hasAnimator,
        PlayRoomTheme theme,
        CancellationToken cancellationToken);
    Task<bool> Delete(Guid id, CancellationToken cancellationToken);
}