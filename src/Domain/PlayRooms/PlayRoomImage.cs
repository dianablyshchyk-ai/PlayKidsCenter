namespace Domain.PlayRooms;

public class PlayRoomImage
{
    public PlayRoomImageId Id { get; }
    public string OriginalName { get; }
    public PlayRoomId PlayRoomId { get; }

    private PlayRoomImage()
    {
        Id = null!;
        OriginalName = null!;
        PlayRoomId = null!;
    }

    private PlayRoomImage(PlayRoomImageId id, string originalName, PlayRoomId playRoomId)
    {
        Id = id;
        OriginalName = originalName;
        PlayRoomId = playRoomId;
    }

    public static PlayRoomImage New(PlayRoomImageId id, string originalName, PlayRoomId playRoomId) => new(id, originalName, playRoomId);
}