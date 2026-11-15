namespace Domain.PlayRooms;

public record PlayRoomImageId(Guid Value)
{
    public static PlayRoomImageId Empty() => new(Guid.Empty);
    public static PlayRoomImageId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}