namespace Domain.PlayRooms;

public record PlayRoomId(Guid Value)
{
    public static PlayRoomId Empty() => new(Guid.Empty);
    public static PlayRoomId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}