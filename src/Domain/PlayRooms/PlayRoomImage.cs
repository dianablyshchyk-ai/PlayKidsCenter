namespace Domain.PlayRooms;

public class PlayRoomImage
{
    public Guid Id { get; }
    public string OriginalName { get; }
    public PlayRoomId PlayRoomId { get; }

    private PlayRoomImage(Guid id, string originalName, PlayRoomId playRoomId)
    {
        Id = id;
        OriginalName = originalName;
        PlayRoomId = playRoomId;
    }

    public static PlayRoomImage New(Guid id, string originalName, PlayRoomId playRoomId) =>
        new(id, originalName, playRoomId);
}