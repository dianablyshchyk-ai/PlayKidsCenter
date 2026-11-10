using Domain.PlayRooms;

namespace Api.Dtos;

public record PlayRoomAttributesDto(
    decimal? AreaSqMeters,
    int? MinRecommendedAge,
    int? MaxRecommendedAge,
    string? ParentNotes)
{
    public static PlayRoomAttributesDto FromDomainModel(PlayRoomAttributes attributes) => new(
        attributes.AreaSqMeters,
        attributes.MinRecommendedAge,
        attributes.MaxRecommendedAge,
        attributes.ParentNotes);

    public PlayRoomAttributes ToDomainModel() => new(AreaSqMeters, MinRecommendedAge, MaxRecommendedAge, ParentNotes);
}

public record PlayRoomDto(
    Guid Id,
    string RoomNumber,
    string Name,
    string Description,
    decimal PricePerHour,
    int Capacity,
    bool HasAnimator,
    PlayRoomTheme Theme,
    PlayRoomAttributesDto Attributes,
    DateTime CreatedAt,
    DateTime? UpdatedAt)
{
    public static PlayRoomDto FromDomainModel(PlayRoom playRoom) => new(
        playRoom.Id.Value,
        playRoom.RoomNumber,
        playRoom.Name,
        playRoom.Description,
        playRoom.PricePerHour,
        playRoom.Capacity,
        playRoom.HasAnimator,
        playRoom.Theme,
        PlayRoomAttributesDto.FromDomainModel(playRoom.Attributes),
        playRoom.CreatedAt,
        playRoom.UpdatedAt);
}

public record CreatePlayRoomDto(
    string RoomNumber,
    string Name,
    string Description,
    decimal PricePerHour,
    int Capacity,
    bool HasAnimator,
    PlayRoomTheme Theme,
    PlayRoomAttributesDto? Attributes);

public record UpdatePlayRoomDto(
    string RoomNumber,
    string Name,
    string Description,
    decimal PricePerHour,
    int Capacity,
    bool HasAnimator,
    PlayRoomTheme Theme,
    PlayRoomAttributesDto? Attributes);