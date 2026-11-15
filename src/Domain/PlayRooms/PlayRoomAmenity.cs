using Domain.Amenities;

namespace Domain.PlayRooms;

public class PlayRoomAmenity
{
    public PlayRoomId PlayRoomId { get; }
    public PlayRoom? PlayRoom { get; }
    public AmenityId AmenityId { get; }
    public Amenity? Amenity { get; }

    private PlayRoomAmenity()
    {
        PlayRoomId = null!;
        AmenityId = null!;
    }

    private PlayRoomAmenity(PlayRoomId playRoomId, AmenityId amenityId)
    {
        PlayRoomId = playRoomId;
        AmenityId = amenityId;
    }

    public static PlayRoomAmenity New(PlayRoomId playRoomId, AmenityId amenityId) => new(playRoomId, amenityId);
}