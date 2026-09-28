using System.ComponentModel.DataAnnotations;
using Domain.PlayRooms;

namespace Api.Dtos;

public record PlayRoomDto(
    Guid Id,
    string RoomNumber,
    string Name,
    decimal PricePerHour,
    int Capacity,
    bool HasAnimator,
    PlayRoomTheme Theme,
    DateTime CreatedAt)
{
    public static PlayRoomDto FromDomainModel(PlayRoom playRoom)
        => new(
            playRoom.Id,
            playRoom.RoomNumber,
            playRoom.Name,
            playRoom.PricePerHour,
            playRoom.Capacity,
            playRoom.HasAnimator,
            playRoom.Theme,
            playRoom.CreatedAt);
}

public record CreatePlayRoomDto(
    [Required, MaxLength(50)] string RoomNumber,
    [Required, MaxLength(255)] string Name,
    [Range(0.01, 1_000_000)] decimal PricePerHour,
    [Range(1, 100)] int Capacity,
    bool HasAnimator,
    [EnumDataType(typeof(PlayRoomTheme))] PlayRoomTheme Theme);

public record UpdatePlayRoomDto(
    [Required, MaxLength(50)] string RoomNumber,
    [Required, MaxLength(255)] string Name,
    [Range(0.01, 1_000_000)] decimal PricePerHour,
    [Range(1, 100)] int Capacity,
    bool HasAnimator,
    [EnumDataType(typeof(PlayRoomTheme))] PlayRoomTheme Theme);