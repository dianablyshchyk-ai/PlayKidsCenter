using Application.Common.Interfaces;
using Application.PlayRooms.Services.Abstract;
using Domain.PlayRooms;

namespace Application.PlayRooms.Services.Implementation;

public class PlayRoomService(IPlayRoomRepository playRoomRepository) : IPlayRoomService
{
    public async Task<IReadOnlyList<PlayRoom>> GetPlayRooms(CancellationToken cancellationToken)
    {
        return await playRoomRepository.GetAll(cancellationToken);
    }

    public async Task<PlayRoom?> GetPlayRoom(Guid id, CancellationToken cancellationToken)
    {
        return await playRoomRepository.GetById(id, cancellationToken);
    }

    public async Task<PlayRoom> Add(
        string roomNumber,
        string name,
        decimal pricePerHour,
        int capacity,
        bool hasAnimator,
        PlayRoomTheme theme,
        CancellationToken cancellationToken)
    {
        var existingRoom = await playRoomRepository.GetByRoomNumber(roomNumber, cancellationToken);
        if (existingRoom is not null)
        {
            throw new ArgumentException($"Play room with number '{roomNumber}' already exists");
        }

        var playRoom = PlayRoom.New(
            Guid.NewGuid(),
            roomNumber,
            name,
            pricePerHour,
            capacity,
            hasAnimator,
            theme);

        return await playRoomRepository.Add(playRoom, cancellationToken);
    }

    public async Task<PlayRoom?> Update(
        Guid id,
        string roomNumber,
        string name,
        decimal pricePerHour,
        int capacity,
        bool hasAnimator,
        PlayRoomTheme theme,
        CancellationToken cancellationToken)
    {
        var playRoom = await playRoomRepository.GetById(id, cancellationToken);
        if (playRoom is null)
        {
            return null;
        }

        var roomWithSameNumber = await playRoomRepository.GetByRoomNumber(roomNumber, cancellationToken);
        if (roomWithSameNumber is not null && roomWithSameNumber.Id != id)
        {
            throw new ArgumentException($"Play room with number '{roomNumber}' already exists");
        }

        playRoom.UpdateDetails(
            roomNumber,
            name,
            pricePerHour,
            capacity,
            hasAnimator,
            theme);

        return await playRoomRepository.Update(playRoom, cancellationToken);
    }

    public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
    {
        var playRoom = await playRoomRepository.GetById(id, cancellationToken);
        if (playRoom is null)
        {
            return false;
        }

        await playRoomRepository.Delete(playRoom, cancellationToken);
        return true;
    }
}