using Domain.Amenities;

namespace Domain.PlayRooms;
public class PlayRoomAmenity
{
    public PlayRoomId PlayRoomId { get; }
    public PlayRoom? PlayRoom { get; }
    public Guid AmenityId { get; }
    public Amenity? Amenity { get; }

    private PlayRoomAmenity(PlayRoomId playRoomId, Guid amenityId)
    {
        PlayRoomId = playRoomId;
        AmenityId = amenityId;
    }

    public static PlayRoomAmenity New(PlayRoomId playRoomId, Guid amenityId) =>
        new(playRoomId, amenityId);
}