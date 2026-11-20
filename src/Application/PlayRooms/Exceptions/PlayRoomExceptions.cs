using Domain.PlayRooms;
namespace Application.PlayRooms.Exceptions;

public abstract class PlayRoomException(PlayRoomId playRoomId, string message, Exception? innerException = null) : Exception(message, innerException)
{
    public PlayRoomId PlayRoomId { get; } = playRoomId;
}
public class PlayRoomAlreadyExistsException(PlayRoomId playRoomId) : PlayRoomException(playRoomId, $"Play room already exists under id {playRoomId}");
public class PlayRoomNotFoundException(PlayRoomId playRoomId) : PlayRoomException(playRoomId, $"Play room not found under id {playRoomId}");
public class PlayRoomUnhandledException(PlayRoomId playRoomId, Exception? innerException = null) : PlayRoomException(playRoomId, $"Unhandled exception under id {playRoomId}", innerException);