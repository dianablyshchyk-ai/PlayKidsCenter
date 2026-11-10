namespace Domain.PlayRooms;

public record PlayRoomAttributes(
    decimal? AreaSqMeters,
    int? MinRecommendedAge,
    int? MaxRecommendedAge,
    string? ParentNotes)
{
    public static PlayRoomAttributes Empty() => new(null, null, null, null);
}